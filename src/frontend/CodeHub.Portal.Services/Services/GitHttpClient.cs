using System.Net.Http.Json;
using System.Text.Json;
using CodeHub.Domain.Git;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class GitHttpClient : IGitHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<GitHttpClient> _logger;

    public GitHttpClient(
        HttpClient httpClient,
        JsonSerializerOptions jsonOptions,
        ILogger<GitHttpClient> logger)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        _logger = logger;
    }

    private const string RepositoryUrl = "repositories";
    private const string PipelineUrl = "pipelines";
    private const string PullRequestUrl = "pull-requests";

    public async Task<List<Pipeline>> GetPipelinesAsync()
    {
        try
        {
            _logger.LogInformation("Getting pipelines from: {Url}", PipelineUrl);
            return await _httpClient.GetFromJsonAsync<List<Pipeline>>(PipelineUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Could not get list of pipelines from {Url}", PullRequestUrl);
            return [];
        }
    }

    public async Task<List<Repository>> GetRepositoriesAsync()
    {
        try
        {
            _logger.LogInformation("Getting repositories from: {Url}", RepositoryUrl);
            return await _httpClient.GetFromJsonAsync<List<Repository>>(RepositoryUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Could not get list of repositories from {Url}", RepositoryUrl);
            return [];
        }
    }

    public async Task<List<PullRequest>> GetPullRequestsAsync()
    {
        try
        {
            _logger.LogInformation("Getting pull requests from: {Url}", PullRequestUrl);
            return await _httpClient.GetFromJsonAsync<List<PullRequest>>(PullRequestUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Could not get list of pull requests from {Url}", PullRequestUrl);
            return [];
        }
    }
}