using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Caching.Memory;

namespace CodeHub.Core.Platforms.Azure;

internal sealed class AzureService : IAzureService
{
    private readonly ArmClient _client = new(new DefaultAzureCredential());
    private readonly IMemoryCache _memoryCache;

    private const string TenantKey = "azure-tenants";
    private const string SubscriptionKey = "azure-subscriptions";

    private static readonly TimeSpan CacheExpirationRelativeToNow = TimeSpan.FromMinutes(10);

    public AzureService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<TenantResource?> GetTenantAsync(string id, CancellationToken cancellationToken)
    {
        var tenants = await GetTenantsAsync(cancellationToken);

        return tenants.Find(ten => ten.Data.DisplayName == id || ten.Data.TenantId.ToString() == id);
    }

    public async Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(TenantKey, async entry =>
        {
            var tenants = new List<TenantResource>();
            await foreach (var tenant in _client.GetTenants().GetAllAsync(cancellationToken))
            {
                tenants.Add(tenant);
            }

            return tenants;
        }) ?? [];
    }

    public async Task<AzureSubscription?> GetSubscriptionAsync(string id, CancellationToken cancellationToken)
    {
        var subscriptions = await GetSubscriptionsAsync(cancellationToken);
        return subscriptions.Find(sub => sub.Name == id || sub.Id == id);
    }

    public async Task<List<AzureSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken)
    {
        var subscriptionResources = await GetSubscriptionsResourceAsync(cancellationToken);

        var azureSubscriptions = new List<AzureSubscription>();

        foreach (var subscriptionResource in subscriptionResources)
        {
            var tenantId = subscriptionResource.Data.TenantId.ToString() ?? string.Empty;
            var tenant = await GetTenantAsync(tenantId, cancellationToken);
            var tenantName = tenant?.Data.DefaultDomain ?? string.Empty;
            var azureSubscription = subscriptionResource.MapToAzureSubscription(tenantName);
            azureSubscriptions.Add(azureSubscription);
        }

        return azureSubscriptions;
    }

    public async Task<List<AzureResource>> GetSubscriptionResourcesAsync(string subscriptionId,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync<List<AzureResource>>(subscriptionId, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var subscriptionResource = await GetSubscriptionResourceAsync(subscriptionId, cancellationToken);

            if (subscriptionResource is null)
            {
                return [];
            }

            var tenantId = subscriptionResource.Data.TenantId.ToString() ?? string.Empty;
            var tenant = await GetTenantAsync(tenantId, cancellationToken);
            var tenantName = tenant?.Data.DefaultDomain ?? string.Empty;

            var azureResources = new List<AzureResource>();

            await foreach (var resource in subscriptionResource.GetGenericResourcesAsync(
                               cancellationToken: cancellationToken))
            {
                var azureResource = resource.Data.MapToAzureResource(tenantName, subscriptionResource.Data.DisplayName);
                azureResources.Add(azureResource);
            }

            return azureResources;
        }) ?? [];
    }

    public async Task<List<AzureResource>> GetAllSubscriptionsResourcesAsync(string[] subscriptionIds,
        CancellationToken cancellationToken)
    {
        var resources = new List<AzureResource>();

        foreach (var subscriptionId in subscriptionIds)
        {
            var subscriptionResources = await GetSubscriptionResourcesAsync(subscriptionId, cancellationToken);
            resources.AddRange(subscriptionResources);
        }

        return resources;
    }


    private async Task<SubscriptionResource?> GetSubscriptionResourceAsync(string id,
        CancellationToken cancellationToken)
    {
        var subscriptions = await GetSubscriptionsResourceAsync(cancellationToken);
        return subscriptions.Find(sub => sub.Data.DisplayName == id || sub.Data.Id.Name == id);
    }

    private async Task<List<SubscriptionResource>> GetSubscriptionsResourceAsync(CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(SubscriptionKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var subscriptionResources = new List<SubscriptionResource>();

            await foreach (var subscription in _client.GetSubscriptions().GetAllAsync(cancellationToken))
            {
                subscriptionResources.Add(subscription);
            }

            return subscriptionResources;
        }) ?? [];
    }

    private async Task GetKeyVaultSecretsAsync(string keyVaultName)
    {
        var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net");
        var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());

        await foreach (var secret in secretClient.GetPropertiesOfSecretsAsync())
        {
        }
    }
}