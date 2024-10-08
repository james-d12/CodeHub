using CodeHub.Core.Platforms.Azure;

namespace CodeHub.Portal.Services.Services;

public interface IAzureHttpClient
{
    Task<List<AzureResource>> GetResourcesAsync();
    Task<List<AzureSubscription>> GetSubscriptionsAsync();
}