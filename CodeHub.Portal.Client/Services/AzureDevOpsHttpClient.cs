using System.Net.Http.Json;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Portal.Client.Services;

public sealed class AzureDevOpsHttpClient(HttpClient httpClient) : IAzureDevOpsHttpClient
{
    public async Task<List<GitRepository>> GetRepositoriesAsync()
    {
        try
        {
            var requestUrl = new Uri($"http://localhost:5104/azure-devops/multipay/repositories");
            var repositories = await httpClient.GetFromJsonAsync<List<GitRepository>>(requestUrl);
            return repositories ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return [];
        }
    }
}