using CodeHub.Domain.Git;
using CodeHub.Domain.Git.Request;
using CodeHub.Domain.Git.Service;
using CodeHub.Module.GitLab.Constants;
using CodeHub.Module.GitLab.Models;
using CodeHub.Module.Shared.Extensions;
using CodeHub.Module.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.GitLab.Services;

public sealed class GitLabGitQueryService : IGitQueryService
{
    private readonly ILogger<GitLabGitQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public GitLabGitQueryService(
        ILogger<GitLabGitQueryService> logger,
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

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from GitLab");
        var gitLabRepositories =
            _memoryCache.Get<List<GitLabRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = gitLabRepositories.ConvertAll<Repository>(p => p);

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.DefaultBranch, p => p.DefaultBranch.EqualsCaseInsensitive(request.DefaultBranch))
            .Where(request.OwnerName, p => p.Owner.Name.EqualsCaseInsensitive(request.OwnerName))
            .ToList();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        _logger.LogInformation("Querying pull requests from GitLab");
        var gitLabPullRequests =
            _memoryCache.Get<List<GitLabPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = gitLabPullRequests.ConvertAll<PullRequest>(p => p);

        return new QueryBuilder<PullRequest>(pullRequests)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Title, p => p.Name.ContainsCaseInsensitive(request.Title))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}