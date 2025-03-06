using Azure.ResourceManager.Resources;
using CodeHub.Core.Azure.Models;

namespace CodeHub.Core.Azure.Services;

public interface IAzureService
{
    Task<TenantResource?> GetTenantAsync(string id, CancellationToken cancellationToken);
    Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken);
    Task<AzureSubscription?> GetSubscriptionAsync(string id, CancellationToken cancellationToken);
    Task<List<AzureSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken);
    Task<List<AzureResource>> GetSubscriptionResourcesAsync(string subscriptionId, CancellationToken cancellationToken);

    Task<List<AzureResource>> GetAllSubscriptionsResourcesAsync(string[] subscriptionIds,
        CancellationToken cancellationToken);
}