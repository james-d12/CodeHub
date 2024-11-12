using Azure.ResourceManager.Resources;
using CodeHub.Core.Platforms.Azure.Models;

namespace CodeHub.Core.Platforms.Azure.Services;

internal interface IAzureCacheService
{
    void SetTenants(List<TenantResource> tenants);
    void SetSubscriptions(List<SubscriptionResource> subscriptionResources);
    void SetResources(List<AzureResource> azureResources, string subscriptionId);
    List<TenantResource> GetTenants();
    List<SubscriptionResource> GetSubscriptions();
    List<AzureResource> GetResources(string subscriptionId);
}