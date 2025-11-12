using System.Collections.Concurrent;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.Security.KeyVault.Secrets;
using CodeHub.Domain.Cloud;
using CodeHub.Module.Azure.Extensions;
using CodeHub.Module.Azure.Models;
using CodeHub.Module.Shared.Extensions;
using CodeHub.Shared;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureService : IAzureService
{
    private readonly ArmClient _client = new(new DefaultAzureCredential());

    public async Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var tenants = new List<TenantResource>();
        await foreach (var tenant in _client.GetTenants().GetAllAsync(cancellationToken))
        {
            tenants.Add(tenant);
        }

        return tenants;
    }

    public async Task<List<SubscriptionResource>> GetSubscriptionsAsync(
        List<string> subscriptionFilters,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var subscriptionResources = new List<SubscriptionResource>();

        await foreach (var subscription in _client.GetSubscriptions().GetAllAsync(cancellationToken))
        {
            subscriptionResources.Add(subscription);
        }

        if (subscriptionFilters.Count <= 0)
        {
            return subscriptionResources;
        }

        return subscriptionResources
            .Where(p => subscriptionFilters.Contains(p.Data.DisplayName, StringComparer.OrdinalIgnoreCase))
            .ToList();
    }

    public async Task<List<AzureCloudResource>> GetResourcesAsync(
        SubscriptionResource subscriptionResource,
        TenantResource tenantResource,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var azureResources = new List<AzureCloudResource>();

        await foreach (var resource in subscriptionResource.GetGenericResourcesAsync(
                           cancellationToken: cancellationToken))
        {
            var azureResource = resource.Data.MapToAzureResource(
                tenantResource.Data.DisplayName,
                subscriptionResource.Data.DisplayName);
            azureResources.Add(azureResource);
        }

        return azureResources;
    }

    public async Task<List<CloudSecret>> GetKeyVaultSecretsAsync(List<AzureCloudResource> resources,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var cloudSecrets = new ConcurrentBag<CloudSecret>();
        var vaults = resources.Where(r => r.Type.EqualsCaseInsensitive("vaults")).ToList();

        var tasks = vaults.Select(async vault =>
        {
            try
            {
                var vaultUri = new Uri($"https://{vault.Id.Value}.vault.azure.net/");
                var client = new SecretClient(vaultUri, new DefaultAzureCredential());

                foreach (var secret in client.GetPropertiesOfSecrets())
                {
                    cloudSecrets.Add(new CloudSecret
                    {
                        Name = secret.Name,
                        Location = vault.Name,
                        Url = secret.VaultUri,
                        Platform = CloudSecretPlatform.Azure
                    });
                }

                await Task.CompletedTask;
            }
            catch (Exception)
            {
                // Log exception if necessary
                // Log.Error(ex, "Error fetching secrets for vault: {VaultName}", vault.Name);
            }
        }).ToList();

        await Task.WhenAll(tasks);

        return cloudSecrets.ToList();
    }
}