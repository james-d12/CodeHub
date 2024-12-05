using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Models.Requests;
using CodeHub.Shared.Models;
using CodeHub.Shared.Models.Requests;
using CodeHub.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Common;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsQueryService : IAzureDevOpsQueryService, IQueryService
{
    private readonly ILogger<AzureDevOpsQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsQueryService(ILogger<AzureDevOpsQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<AzureDevOpsPipeline> QueryPipelines(AzureDevOpsQueryPipelineRequest request)
    {
        _logger.LogInformation("Querying pipelines with Request: {Request}", request);
        var pipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];

        if (pipelines.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<AzureDevOpsPipeline>(pipelines)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.ProjectId, p => p.ProjectId == request.ProjectId)
            .Where(request.ProjectName, p => p.ProjectName.Contains(request.ProjectName ?? string.Empty))
            .ToList();
    }

    public List<Pipeline> QueryPipelines(QueryPipelineRequest request)
    {
        var azureDevOpsPipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = azureDevOpsPipelines.ConvertAll(p => (Pipeline)p);

        if (azureDevOpsPipelines.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Url, p => p.Url == new Uri(request.Url ?? string.Empty))
            .ToList();
    }

    public List<AzureDevOpsRepository> QueryRepositories(AzureDevOpsQueryRepositoryRequest request)
    {
        _logger.LogInformation("Querying repositories");
        var repositories = _memoryCache.Get<List<AzureDevOpsRepository>>(CacheConstants.RepositoryCacheKey) ?? [];

        if (repositories.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<AzureDevOpsRepository>(repositories)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.ProjectId, p => p.ProjectId == request.ProjectId)
            .Where(request.ProjectName, p => p.ProjectName.Contains(request.ProjectName ?? string.Empty))
            .ToList();
    }

    public List<Repository> QueryRepositories(QueryRepositoryRequest request)
    {
        _logger.LogInformation("Querying repositories");
        var azureDevOpsRepositories =
            _memoryCache.Get<List<AzureDevOpsRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = azureDevOpsRepositories.ConvertAll(p => (Repository)p);

        if (repositories.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .ToList();
    }

    public List<AzureDevOpsProject> QueryProjects()
    {
        _logger.LogInformation("Querying projects");
        return _memoryCache.Get<List<AzureDevOpsProject>>(CacheConstants.ProjectCacheKey) ?? [];
    }

    public List<AzureDevOpsTeam> QueryTeams()
    {
        _logger.LogInformation("Querying Teams");
        return _memoryCache.Get<List<AzureDevOpsTeam>>(CacheConstants.TeamCacheKey) ?? [];
    }

    public List<AzureDevOpsPullRequest> QueryPullRequests()
    {
        _logger.LogInformation("Querying pull requests");
        return _memoryCache.Get<List<AzureDevOpsPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
    }

    public List<PullRequest> QueryPullRequests(QueryPullRequestRequest request)
    {
        var azureDevOpsPullRequests =
            _memoryCache.Get<List<AzureDevOpsPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = azureDevOpsPullRequests.ConvertAll(p => (PullRequest)p);
        return pullRequests;
    }

    public List<AzureDevOpsWorkItem> QueryWorkItems()
    {
        _logger.LogInformation("Querying work items");
        return _memoryCache.Get<List<AzureDevOpsWorkItem>>(CacheConstants.WorkItemsCacheKey) ?? [];
    }
}