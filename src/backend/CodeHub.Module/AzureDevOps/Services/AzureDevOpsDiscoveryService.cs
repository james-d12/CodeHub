using CodeHub.Domain.Discovery;
using CodeHub.Module.AzureDevOps.Constants;
using CodeHub.Module.AzureDevOps.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.AzureDevOps.Services;

public sealed class AzureDevOpsDiscoveryService : DiscoveryService
{
    private readonly IAzureDevOpsService _azureDevOpsService;
    private readonly ILogger<AzureDevOpsDiscoveryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsDiscoveryService(
        ILogger<AzureDevOpsDiscoveryService> logger,
        IAzureDevOpsService azureDevOpsService,
        IMemoryCache memoryCache) : base(logger)
    {
        _logger = logger;
        _azureDevOpsService = azureDevOpsService;
        _memoryCache = memoryCache;
    }

    public override string Platform => "Azure DevOps";

    protected override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Discovering Azure DevOps team resources...");
        var teams = await _azureDevOpsService.GetTeamsAsync(cancellationToken);

        _logger.LogInformation("Discovering Azure DevOps project resources...");
        var projects = await _azureDevOpsService.GetProjectsAsync(cancellationToken);

        var pipelines = new List<AzureDevOpsPipeline>();
        var repositories = new List<AzureDevOpsRepository>();
        var pullRequests = new List<AzureDevOpsPullRequest>();
        var workItems = new List<AzureDevOpsWorkItem>();

        foreach (var project in projects)
        {
            _logger.LogInformation("Discovering Azure DevOps Repository resources for {ProjectName}", project.Name);
            var projectRepositories =
                await _azureDevOpsService.GetRepositoriesAsync(project.Id, cancellationToken);
            repositories.AddRange(projectRepositories);

            _logger.LogInformation("Discovering Azure DevOps Pipeline resources for {ProjectName}", project.Name);
            var projectPipelines = await _azureDevOpsService.GetPipelinesAsync(project.Id, cancellationToken);
            pipelines.AddRange(projectPipelines);

            _logger.LogInformation("Discovering Azure DevOps Pipeline resources for {ProjectName}", project.Name);
            var projectPullRequests = await _azureDevOpsService.GetPullRequestsAsync(project.Id, cancellationToken);
            pullRequests.AddRange(projectPullRequests);

            _logger.LogInformation("Discovering Azure DevOps Work Item resources for {ProjectName}", project.Name);
            var projectWorkItems = await _azureDevOpsService.GetWorkItemsAsync(project.Name, cancellationToken);
            workItems.AddRange(projectWorkItems);
        }

        _memoryCache.Set(AzureDevOpsCacheConstants.ProjectCacheKey, projects);
        _memoryCache.Set(AzureDevOpsCacheConstants.TeamCacheKey, teams);
        _memoryCache.Set(AzureDevOpsCacheConstants.PipelineCacheKey, pipelines);
        _memoryCache.Set(AzureDevOpsCacheConstants.RepositoryCacheKey, repositories);
        _memoryCache.Set(AzureDevOpsCacheConstants.PullRequestCacheKey, pullRequests);
        _memoryCache.Set(AzureDevOpsCacheConstants.WorkItemsCacheKey, workItems);
    }
}