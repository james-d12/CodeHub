using System.Net.Http.Json;
using System.Text.Json;
using CodeHub.Domain.Cloud;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class CloudHttpClient : ICloudHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<ResourceHttpClient> _logger;

    private const string CloudResourcesUrl = "resources";

    public CloudHttpClient(
        HttpClient httpClient,
        JsonSerializerOptions jsonOptions,
        ILogger<ResourceHttpClient> logger)
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
}