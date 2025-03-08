using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using CodeHub.Module.Azure.Extensions;
using CodeHub.Module.Azure.Models;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureService : IAzureService
{
    private readonly ILogger<AzureService> _logger;
    private readonly ArmClient _client = new(new DefaultAzureCredential());

    public AzureService(ILogger<AzureService> logger)
    {
        _logger = logger;
    }


    public async Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = new List<TenantResource>();
        await foreach (var tenant in _client.GetTenants().GetAllAsync(cancellationToken))
        {
            tenants.Add(tenant);
        }

        return tenants;
    }


    public async Task<List<SubscriptionResource>> GetSubscriptionsAsyncV2(CancellationToken cancellationToken)
    {
        var subscriptionResources = new List<SubscriptionResource>();

        await foreach (var subscription in _client.GetSubscriptions().GetAllAsync(cancellationToken))
        {
            subscriptionResources.Add(subscription);
        }

        return subscriptionResources;
    }

    public async Task<List<AzureResource>> GetResourcesAsync(
        SubscriptionResource subscriptionResource,
        TenantResource tenantResource,
        CancellationToken cancellationToken)
    {
        var azureResources = new List<AzureResource>();

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
}