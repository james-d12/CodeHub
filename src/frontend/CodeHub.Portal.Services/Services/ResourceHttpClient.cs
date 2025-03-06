using System.Net.Http.Json;
using CodeHub.Core.Shared.Models;

namespace CodeHub.Portal.Services.Services;

public sealed class ResourceHttpClient(HttpClient httpClient) : IResourceHttpClient
{
    private const string RepositoryUrl = "repositories";
    private const string PipelineUrl = "pipelines";
    private const string PullRequestUrl = "pull-requests";

    public async Task<List<Pipeline>> GetPipelines()
    {
        var resources = await httpClient.GetFromJsonAsync<List<Pipeline>>(PipelineUrl);
        return resources ?? [];
    }

    public async Task<List<Repository>> GetRepositories()
    {
        return await httpClient.GetFromJsonAsync<List<Repository>>(RepositoryUrl) ?? [];
    }

    public async Task<List<PullRequest>> GetPullRequests()
    {
        return await httpClient.GetFromJsonAsync<List<PullRequest>>(PullRequestUrl) ?? [];
    }
}