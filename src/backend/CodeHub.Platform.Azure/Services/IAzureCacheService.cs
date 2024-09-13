using Azure.ResourceManager.Resources;
using CodeHub.Platform.Azure.Models;

namespace CodeHub.Platform.Azure.Services;

internal interface IAzureCacheService
{
    void SetTenants(List<TenantResource> tenants);
    void SetSubscriptions(List<SubscriptionResource> subscriptionResources);
    void SetResources(List<AzureResource> azureResources, string subscriptionId);
    List<TenantResource> GetTenants();
    List<SubscriptionResource> GetSubscriptions();
    List<AzureResource> GetResources(string subscriptionId);
}