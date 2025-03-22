using System.Net.Http.Json;
using System.Text.Json;
using CodeHub.Domain.Cloud;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class CloudHttpClient : ICloudHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<CloudHttpClient> _logger;

    private const string CloudResourcesUrl = "resources";
    private const string CloudSecretsUrl = "secrets";

    public CloudHttpClient(
        HttpClient httpClient,
        JsonSerializerOptions jsonOptions,
        ILogger<CloudHttpClient> logger)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        _logger = logger;
    }


    public async Task<List<CloudResource>> GetCloudResourcesAsync()
    {
        try
        {
            _logger.LogInformation("Getting cloud resources from: {Url}", CloudResourcesUrl);
            return await _httpClient.GetFromJsonAsync<List<CloudResource>>(CloudResourcesUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Could not get list of cloud resources from {Url}", CloudResourcesUrl);
            return [];
        }
    }

    public async Task<List<CloudSecret>> GetCloudSecretsAsync()
    {
        try
        {
            _logger.LogInformation("Getting cloud secrets from: {Url}", CloudSecretsUrl);
            return await _httpClient.GetFromJsonAsync<List<CloudSecret>>(CloudSecretsUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Could not get list of cloud secrets from {Url}", CloudSecretsUrl);
            return [];
        }
    }
}