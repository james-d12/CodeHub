using CodeHub.Engine.Azure.Models;

namespace CodeHub.Portal.Client.Services;

public interface IAzureHttpClient
{
    Task<List<AzureResource>> GetResourcesAsync();
    Task<List<AzureSubscription>> GetSubscriptionsAsync();
}