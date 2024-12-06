using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Shared.Models;
using CodeHub.Shared.Query;
using CodeHub.Shared.Query.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Common;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsQueryService : IQueryService
{
    private readonly ILogger<AzureDevOpsQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsQueryService(ILogger<AzureDevOpsQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<Pipeline> QueryPipelines(PipelineQueryRequest request)
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

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
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

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        var azureDevOpsPullRequests =
            _memoryCache.Get<List<AzureDevOpsPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = azureDevOpsPullRequests.ConvertAll(p => (PullRequest)p);

        if (pullRequests.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<PullRequest>(pullRequests)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Title, p => p.Name.Contains(request.Title ?? string.Empty))
            .ToList();
    }
}