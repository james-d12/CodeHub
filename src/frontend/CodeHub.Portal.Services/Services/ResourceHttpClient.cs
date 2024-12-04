using System.Net.Http.Json;
using CodeHub.Shared.Models;

namespace CodeHub.Portal.Services.Services;

public sealed class ResourceHttpClient(HttpClient httpClient) : IResourceHttpClient
{
    private const string RepositoryUrl = "repositories";
    private const string PipelineUrl = "pipelines";

    public async Task<List<Pipeline>> GetPipelines()
    {
        var resources = await httpClient.GetFromJsonAsync<List<Pipeline>>(PipelineUrl);
        return resources ?? [];
    }

    public async Task<List<Repository>> GetRepositories()
    {
        var resources = await httpClient.GetFromJsonAsync<List<Repository>>(RepositoryUrl);
        return resources ?? [];
    }
}