using CodeHub.Platform.Azure.Models;
using CodeHub.Portal.Services.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureHttpClient
{
    Task<List<AzureResource>> GetResourcesAsync();
    Task<List<AzureSubscriptionResponse>> GetSubscriptionsAsync();
}