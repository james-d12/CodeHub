using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Shared.Models;
using CodeHub.Shared.Query;
using CodeHub.Shared.Query.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

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
        _logger.LogInformation("Querying pipelines from Azure DevOps");
        var azureDevOpsPipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = azureDevOpsPipelines.ConvertAll(p => (Pipeline)p);

        if (azureDevOpsPipelines.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Url, p => p.Url == new Uri(request.Url ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from Azure DevOps");
        var azureDevOpsRepositories =
            _memoryCache.Get<List<AzureDevOpsRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = azureDevOpsRepositories.ConvertAll(p => (Repository)p);

        if (repositories.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        _logger.LogInformation("Querying pull requests from Azure DevOps");
        var azureDevOpsPullRequests =
            _memoryCache.Get<List<AzureDevOpsPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = azureDevOpsPullRequests.ConvertAll(p => (PullRequest)p);

        if (pullRequests.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<PullRequest>(pullRequests)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Title, p => p.Name.Contains(request.Title ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}