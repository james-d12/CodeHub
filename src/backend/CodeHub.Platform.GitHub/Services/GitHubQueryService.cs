using CodeHub.Platform.GitHub.Constants;
using CodeHub.Shared.Models;
using CodeHub.Shared.Query;
using CodeHub.Shared.Query.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubQueryService : IQueryService
{
    private readonly ILogger<GitHubQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public GitHubQueryService(ILogger<GitHubQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<Pipeline> QueryPipelines(PipelineQueryRequest request)
    {
        _logger.LogInformation("Querying pipelines from GitHub");
        var pipelines =
            _memoryCache.Get<List<Pipeline>>(CacheConstants.PipelineCacheKey) ?? [];

        if (pipelines.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from GitHub");
        var repositories =
            _memoryCache.Get<List<Repository>>(CacheConstants.RepositoryCacheKey) ?? [];

        if (repositories.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        return [];
    }
}