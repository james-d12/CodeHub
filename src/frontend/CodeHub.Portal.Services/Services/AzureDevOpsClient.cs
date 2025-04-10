using System.Net.Http.Json;
using System.Text.Json;
using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Shared;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class AzureDevOpsClient : IAzureDevOpsClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<AzureDevOpsClient> _logger;

    public AzureDevOpsClient(
        HttpClient httpClient,
        JsonSerializerOptions jsonOptions,
        ILogger<AzureDevOpsClient> logger)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        _logger = logger;
    }

    public async Task<AzureDevOpsRepository?> GetRepositoryAsync(string name)
    {
        using var activity = Tracing.StartActivity();
        try
        {
            var url = $"repositories/{name}";
            _logger.LogInformation("Getting Azure DevOps Repository with {Name} from: {Url}", name, url);
            return await _httpClient.GetFromJsonAsync<AzureDevOpsRepository>(url, _jsonOptions);
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Could not get Azure DevOps Repository with {Name}", name);
            return null;
        }
    }

    public async Task<AzureDevOpsPipeline?> GetPipelineAsync(string name)
    {
        using var activity = Tracing.StartActivity();
        try
        {
            var url = $"pipelines/{name}";
            _logger.LogInformation("Getting Azure DevOps Pipeline with {Name} from: {Url}", name, url);
            return await _httpClient.GetFromJsonAsync<AzureDevOpsPipeline>(url, _jsonOptions);
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Could not get Azure DevOps Pipeline with {Name}", name);
            return null;
        }
    }
}