using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsService : IAzureDevOpsService
{
    private readonly IAzureDevOpsConnectionService _azureDevOpsConnectionService;
    private readonly IMemoryCache _memoryCache;

    private const string RepositoryCacheKey = "azure-devops-repositories";
    private const string PipelineCacheKey = "azure-devops-pipelines";
    private const string ProjectCacheKey = "azure-devops-projects";
    private const string TeamCacheKey = "azure-devops-teams";
    private const string WorkItemsCacheKey = "azure-devops-workitems";

    private static readonly TimeSpan CacheExpirationRelativeToNow = TimeSpan.FromMinutes(10);

    public AzureDevOpsService(IAzureDevOpsConnectionService azureDevOpsConnectionService, IMemoryCache memoryCache)
    {
        _azureDevOpsConnectionService = azureDevOpsConnectionService;
        _memoryCache = memoryCache;
    }

    public async Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(string projectName,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync($"{RepositoryCacheKey}-{projectName}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var gitClient = await _azureDevOpsConnectionService.GetClientAsync<GitHttpClient>(cancellationToken);
            var repositories =
                await gitClient.GetRepositoriesAsync(projectName, cancellationToken: cancellationToken) ?? [];
            return repositories.Select(r => r.MapToAzureDevOpsRepository()).ToList();
        }) ?? [];
    }

    public async Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(string projectName,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync($"{PipelineCacheKey}-{projectName}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var buildClient = await _azureDevOpsConnectionService.GetClientAsync<BuildHttpClient>(cancellationToken);
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
            var projectClient =
                await _azureDevOpsConnectionService.GetClientAsync<ProjectHttpClient>(cancellationToken);
            var results = await projectClient.GetProjects();
            return results.Select(pr => pr.MapToAzureDevOpsProject()).ToList();
        }) ?? [];
    }

    public async Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(TeamCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;
            var teamClient = await _azureDevOpsConnectionService.GetClientAsync<TeamHttpClient>(cancellationToken);
            var teams = await teamClient.GetAllTeamsAsync(cancellationToken: cancellationToken);
            return teams.Select(t => t.MapToAzureDevOpsTeam()).ToList();
        }) ?? [];
    }

    public async Task<List<WorkItem>> GetWorkItemsAsync(string projectName, CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(WorkItemsCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheExpirationRelativeToNow;

            var workItemTrackingClient =
                await _azureDevOpsConnectionService.GetClientAsync<WorkItemTrackingHttpClient>(cancellationToken);
            var wiql = new Wiql
            {
                Query = $@"
                    SELECT [System.Id], [System.Title], [System.State]
                    FROM WorkItems
                    WHERE [System.TeamProject] = '{projectName}'"
            };

            var queryResult = await workItemTrackingClient.QueryByWiqlAsync(wiql, cancellationToken: cancellationToken);

            if (queryResult is null || !queryResult.WorkItems.Any())
            {
                return [];
            }

            var workItemIds = queryResult.WorkItems.Select(item => item.Id).ToList();
            var workItems = new List<WorkItem>();
            const int batchSize = 200;

            for (var i = 0; i < workItemIds.Count; i += batchSize)
            {
                var batchIds = workItemIds.Skip(i).Take(batchSize).ToArray();
                var batchWorkItems =
                    await workItemTrackingClient.GetWorkItemsAsync(batchIds, cancellationToken: cancellationToken);
                workItems.AddRange(batchWorkItems);
            }

            return workItems;
        }) ?? [];
    }
}