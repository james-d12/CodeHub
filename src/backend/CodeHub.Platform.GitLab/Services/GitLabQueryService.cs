using CodeHub.Platform.GitLab.Constants;
using CodeHub.Platform.GitLab.Models;
using CodeHub.Shared.Models;
using CodeHub.Shared.Query;
using CodeHub.Shared.Query.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitLab.Services;

internal sealed class GitLabQueryService : IQueryService
{
    private readonly ILogger<GitLabQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public GitLabQueryService(
        ILogger<GitLabQueryService> logger,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<Pipeline> QueryPipelines(PipelineQueryRequest request)
    {
        _logger.LogInformation("Querying pipelines from GitLab");
        var gitLabPipelines = _memoryCache.Get<List<GitLabPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = gitLabPipelines.ConvertAll<Pipeline>(p => p);

        if (gitLabPipelines.Count <= 0)
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
        throw new NotImplementedException();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        _logger.LogInformation("Querying pull requests from GitLab");
        var gitLabPullRequests =
            _memoryCache.Get<List<GitLabPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = gitLabPullRequests.ConvertAll<PullRequest>(p => p);

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