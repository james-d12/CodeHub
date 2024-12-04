using System.Net.Http.Json;
using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class AzureDevOpsHttpClient : IAzureDevOpsHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AzureDevOpsHttpClient> _logger;

    public AzureDevOpsHttpClient(ILogger<AzureDevOpsHttpClient> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<List<AzureDevOpsRepository>> GetRepositoriesAsync()
    {
        try
        {
            _logger.LogInformation("Attempting to get all repositories from azure devops.");
            var repositories = await _httpClient.GetFromJsonAsync<List<AzureDevOpsRepository>>("/repositories");

            _logger.LogInformation("Retrieved: {Count}", repositories?.Count);

            return repositories ?? [];
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return [];
        }
    }
}