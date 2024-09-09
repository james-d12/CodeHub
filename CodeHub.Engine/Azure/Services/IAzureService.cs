using Azure.ResourceManager.Resources;
using CodeHub.Engine.Azure.Models;

namespace CodeHub.Engine.Azure.Services;

public interface IAzureService
{
    Task<TenantResource?> GetTenantAsync(string id, CancellationToken cancellationToken);
    Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken);
    Task<SubscriptionResource?> GetSubscriptionAsync(string id, CancellationToken cancellationToken);
    Task<List<SubscriptionResource>> GetSubscriptionsAsync(CancellationToken cancellationToken);
    Task<List<AzureResource>> GetSubscriptionResourcesAsync(string subscriptionId, CancellationToken cancellationToken);

    Task<List<AzureResource>> GetAllSubscriptionsResourcesAsync(string[] subscriptionIds,
        CancellationToken cancellationToken);
}