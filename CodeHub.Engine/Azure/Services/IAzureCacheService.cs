using Azure.ResourceManager.Resources;
using CodeHub.Engine.Azure.Models;

namespace CodeHub.Engine.Azure.Services;

internal interface IAzureCacheService
{
    void SetTenants(List<TenantResource> tenants);
    void SetSubscriptions(List<SubscriptionResource> subscriptionResources);
    void SetResources(List<AzureResource> azureResources, string subscriptionId);
    List<TenantResource> GetTenants();
    List<SubscriptionResource> GetSubscriptions();
    List<AzureResource> GetResources(string subscriptionId);
}