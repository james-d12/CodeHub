using System.Net.Http.Json;
using System.Web;
using CodeHub.Core.Platforms.Azure.Models;

namespace CodeHub.Portal.Services.Services;

public sealed class AzureHttpClient(HttpClient httpClient) : IAzureHttpClient
{
    public async Task<List<AzureResource>> GetResourcesAsync()
    {
        try
        {
            var subscriptions = await GetSubscriptionsAsync();
            var subscriptionNames = subscriptions.Select(subscription => subscription.Name).ToList();
            var queryString = GetQueryString(subscriptionNames);
            var requestUrl = "subscriptions/resources?" + queryString;
            var resources = await httpClient.GetFromJsonAsync<List<AzureResource>>(requestUrl);
            return resources ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return [];
        }
    }

    public async Task<List<AzureSubscription>> GetSubscriptionsAsync()
    {
        try
        {
            var subscriptions =
                await httpClient.GetFromJsonAsync<List<AzureSubscription>>("subscriptions");
            return subscriptions ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error: {0}", exception);
            return [];
        }
    }

    private static string GetQueryString(List<string> subscriptions)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var sub in subscriptions)
        {
            query.Add("subscriptionIds", sub);
        }

        return query.ToString() ?? string.Empty;
    }
}