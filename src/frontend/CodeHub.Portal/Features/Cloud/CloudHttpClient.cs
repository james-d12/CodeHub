using System.Text.Json;
using CodeHub.Domain.Cloud;
using CodeHub.Shared;

namespace CodeHub.Portal.Features.Cloud;

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
        using var activity = Tracing.StartActivity();
        try
        {
            _logger.LogInformation("Getting cloud resources from: {Url}", CloudResourcesUrl);
            return await _httpClient.GetFromJsonAsync<List<CloudResource>>(CloudResourcesUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Could not get list of cloud resources from {Url}", CloudResourcesUrl);
            return [];
        }
    }

    public async Task<List<CloudSecret>> GetCloudSecretsAsync()
    {
        using var activity = Tracing.StartActivity();
        try
        {
            _logger.LogInformation("Getting cloud secrets from: {Url}", CloudSecretsUrl);
            return await _httpClient.GetFromJsonAsync<List<CloudSecret>>(CloudSecretsUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Could not get list of cloud secrets from {Url}", CloudSecretsUrl);
            return [];
        }
    }
}