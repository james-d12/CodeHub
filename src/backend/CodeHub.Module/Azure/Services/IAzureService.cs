using Azure.ResourceManager.Resources;
using CodeHub.Module.Azure.Models;

namespace CodeHub.Module.Azure.Services;

public interface IAzureService
{
    Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken);

    Task<List<AzureCloudResource>> GetResourcesAsync(
        SubscriptionResource subscriptionResource,
        TenantResource tenantResource,
        CancellationToken cancellationToken);

    Task<List<SubscriptionResource>> GetSubscriptionsAsync(CancellationToken cancellationToken);
}