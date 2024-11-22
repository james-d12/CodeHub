using System.Net.Http.Json;
using CodeHub.Core.Platforms.AzureDevOps.Models;

namespace CodeHub.Portal.Services.Services;

public sealed class AzureDevOpsHttpClient(HttpClient httpClient) : IAzureDevOpsHttpClient
{
    public async Task<List<AzureDevOpsRepository>> GetRepositoriesAsync()
    {
        try
        {
            var repositories = await httpClient.GetFromJsonAsync<List<AzureDevOpsRepository>>("multipay/repositories");
            return repositories ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return [];
        }
    }
}