using CodeHub.Domain.Git;
using CodeHub.Module.GitHub.Constants;
using CodeHub.Module.GitHub.Models;
using CodeHub.Module.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.GitHub.Services;

public sealed class GitHubGitQueryService : IGitQueryService
{
    private readonly ILogger<GitHubGitQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public GitHubGitQueryService(ILogger<GitHubGitQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<Pipeline> QueryPipelines(PipelineQueryRequest request)
    {
        _logger.LogInformation("Querying pipelines from GitHub");
        var githubPipelines = _memoryCache.Get<List<GitHubPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = githubPipelines.ConvertAll<Pipeline>(p => p);

        if (pipelines.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from GitHub");
        var gitHubRepositories = _memoryCache.Get<List<GitHubRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = gitHubRepositories.ConvertAll<Repository>(p => p);

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
        _logger.LogInformation("Querying pull requests from GitHub");
        var githubPullRequests = _memoryCache.Get<List<GitHubPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = githubPullRequests.ConvertAll<PullRequest>(p => p);

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