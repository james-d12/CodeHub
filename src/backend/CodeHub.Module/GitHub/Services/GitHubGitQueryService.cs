﻿using CodeHub.Domain.Git;
using CodeHub.Domain.Git.Request;
using CodeHub.Domain.Git.Service;
using CodeHub.Module.GitHub.Constants;
using CodeHub.Module.GitHub.Models;
using CodeHub.Module.Shared.Extensions;
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

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from GitHub");
        var gitHubRepositories = _memoryCache.Get<List<GitHubRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = gitHubRepositories.ConvertAll<Repository>(p => p);

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.DefaultBranch, p => p.DefaultBranch.EqualsCaseInsensitive(request.DefaultBranch))
            .ToList();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        _logger.LogInformation("Querying pull requests from GitHub");
        var githubPullRequests = _memoryCache.Get<List<GitHubPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = githubPullRequests.ConvertAll<PullRequest>(p => p);

        return new QueryBuilder<PullRequest>(pullRequests)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Title, p => p.Name.ContainsCaseInsensitive(request.Title))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}