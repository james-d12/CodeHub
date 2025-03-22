using CodeHub.Domain.Discovery;
using CodeHub.Module.GitLab.Extensions;
using CodeHub.Module.GitLab.Constants;
using CodeHub.Module.GitLab.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.GitLab.Services;

public sealed class GitLabDiscoveryService : DiscoveryService
{
    private readonly IGitLabService _gitLabService;
    private readonly IMemoryCache _memoryCache;

    public GitLabDiscoveryService(
        ILogger<GitLabDiscoveryService> logger,
        IGitLabService gitLabService,
        IMemoryCache memoryCache) : base(logger)
    {
        _gitLabService = gitLabService;
        _memoryCache = memoryCache;
    }

    public override string Platform => "GitLab";

    protected override Task StartAsync(CancellationToken cancellationToken)
    {
        var projects = _gitLabService.GetProjects();

        var repositories = projects.Select(p => p.MapToGitLabRepository()).ToList();
        var pullRequests = _gitLabService.GetPullRequests();

        var pipelines = new List<GitLabPipeline>();
        foreach (var project in projects)
        {
            var projectPipelines = _gitLabService.GetPipelines(project);
            pipelines.AddRange(projectPipelines);
        }

        _memoryCache.Set(GitLabCacheConstants.PipelineCacheKey, pipelines);
        _memoryCache.Set(GitLabCacheConstants.PullRequestCacheKey, pullRequests);
        _memoryCache.Set(GitLabCacheConstants.RepositoryCacheKey, repositories);

        return Task.FromResult(true);
    }
}