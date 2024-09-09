using System.Net.Http.Json;
using System.Web;
using CodeHub.Engine.Azure.Models;

namespace CodeHub.Portal.Client.Services;

public sealed class AzureHttpClient(HttpClient httpClient) : IAzureHttpClient
{
    public async Task<List<AzureResource>> GetResourcesAsync()
    {
        try
        {
            var resources = new List<AzureResource>();
            
            var subs = new List<string>([
                "PayPoint_Dev_MultiPay_Developers (CSP)",
                //"PayPoint_Dev_PreProd_SharedServices (CSP)",
                //"PayPoint_Dev_PreProd_MultiPay (CSP)",
                //"PayPoint_Dev_ProductionSystems_MultiPay (CSP)"
            ]);

            foreach (var subscription in subs)
            {
                var queryString = GetQueryString(subs);
                var requestUrl = "http://localhost:5104/azure/subscriptions/resources?" + queryString;

                Console.WriteLine(requestUrl);

                var subResources =
                    await httpClient.GetFromJsonAsync<List<AzureResource>>(requestUrl);
                resources.AddRange(subResources ?? []);
            }

            return resources;
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
            var requestUrl = "http://localhost:5104/azure/subscriptions";
            var subscriptions =
                await httpClient.GetFromJsonAsync<List<AzureSubscription>>(requestUrl);
            return subscriptions ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return [];
        }
    }

    private static string GetQueryString(List<string> subscriptions)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var sub in subscriptions)
        {
            query["subscriptionIds"] = sub;
        }

        return query.ToString() ?? string.Empty;
    }
}