using Azure.ResourceManager.Resources;
using CodeHub.Domain.Cloud;
using CodeHub.Module.Azure.Models;

namespace CodeHub.Module.Azure.Services;

public interface IAzureService
{
    Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken);

    Task<List<AzureCloudResource>> GetResourcesAsync(
        SubscriptionResource subscriptionResource,
        TenantResource tenantResource,
        CancellationToken cancellationToken);

    Task<List<SubscriptionResource>> GetSubscriptionsAsync(
        List<string> subscriptionFilters,
        CancellationToken cancellationToken);

    Task<List<CloudSecret>> GetKeyVaultSecretsAsync(List<AzureCloudResource> resources,
        CancellationToken cancellationToken);
}