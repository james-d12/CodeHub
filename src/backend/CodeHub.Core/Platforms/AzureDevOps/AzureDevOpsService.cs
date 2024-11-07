using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace CodeHub.Core.Platforms.AzureDevOps;

internal sealed class AzureDevOpsService : IAzureDevOpsService
{
    private readonly VssConnection _connection;
    private readonly IMemoryCache _memoryCache;

    private const string RepositoryCacheKey = "azure-devops-repositories";
    private const string PipelineCacheKey = "azure-devops-pipelines";
    private const string ProjectCacheKey = "azure-devops-projects";
    private const string TeamCacheKey = "azure-devops-teams";

    private static readonly TimeSpan CacheExpirationRelativeToNow = TimeSpan.FromMinutes(10);

    public AzureDevOpsService(IMemoryCache memoryCache, IOptions<AzureDevOpsSettings> options)
    {
        _memoryCache = memoryCache;
        var connectionUri = new Uri($"https://dev.azure.com/{options.Value.Organization}");
        var credentials = new VssBasicCredential(string.Empty, options.Value.PersonalAccessToken);
        _connection = new VssConnection(connectionUri, credentials);
    }

    public async Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(string projectName,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(RepositoryCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var gitClient = await _connection.GetClientAsync<GitHttpClient>(cancellationToken);
            var repositories =
                await gitClient.GetRepositoriesAsync(projectName, cancellationToken: cancellationToken) ?? [];
            return repositories.Select(r => r.MapToAzureDevOpsRepository()).ToList();
        }) ?? [];
    }

    public async Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(string projectName,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(PipelineCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var buildClient = await _connection.GetClientAsync<BuildHttpClient>(cancellationToken);
            var pipelines = await buildClient.GetDefinitionsAsync(projectName, cancellationToken: cancellationToken);
            var azureDevopsPipelines =
                pipelines.Select(p => p.MapToAzureDevOpsPipeline()).ToList();
            return azureDevopsPipelines;
        }) ?? [];
    }

    public async Task<List<AzureDevOpsProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(ProjectCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var projectClient = await _connection.GetClientAsync<ProjectHttpClient>(cancellationToken);
            var results = await projectClient.GetProjects();
            return results.Select(pr => pr.MapToAzureDevOpsProject()).ToList();
        }) ?? [];
    }

    public async Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(TeamCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var teamClient = await _connection.GetClientAsync<TeamHttpClient>(cancellationToken);
            var teams = await teamClient.GetAllTeamsAsync(cancellationToken: cancellationToken);
            return teams.Select(t => t.MapToAzureDevOpsTeam()).ToList();
        }) ?? [];
    }
}