using Azure.ResourceManager.Resources;
using CodeHub.Engine.Azure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CodeHub.Engine.Azure.Services;

internal sealed class AzureCacheService(IMemoryCache memoryCache) : IAzureCacheService
{
    private const string TenantKey = "azure-tenants";
    private const string SubscriptionKey = "azure-subscriptions";
    
    public void SetTenants(List<TenantResource> tenants)
    {
        SetItem(tenants, TenantKey);
    }

    public void SetSubscriptions(List<SubscriptionResource> subscriptionResources)
    {
        SetItem(subscriptionResources, SubscriptionKey);
    }

    public void SetResources(List<AzureResource> azureResources, string subscriptionId)
    {
        SetItem(azureResources, subscriptionId);
    }
    
    public List<TenantResource> GetTenants()
    {
        return memoryCache.Get<List<TenantResource>>(TenantKey) ?? [];
    }
    
    public List<SubscriptionResource> GetSubscriptions()
    {
        return memoryCache.Get<List<SubscriptionResource>>(SubscriptionKey) ?? [];
    }

    public List<AzureResource> GetResources(string subscriptionId)
    {
        return memoryCache.Get<List<AzureResource>>(subscriptionId) ?? [];
    }

    private void SetItem<T>(T item, string id)
    {
        if (!memoryCache.TryGetValue(id, out _))
        {
            memoryCache.Set(id, item);
        }
    }
    
}