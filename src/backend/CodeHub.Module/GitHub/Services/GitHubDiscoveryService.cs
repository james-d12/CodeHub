using CodeHub.Domain.Discovery;
using CodeHub.Module.GitHub.Constants;
using CodeHub.Module.GitHub.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.GitHub.Services;

public sealed class GitHubDiscoveryService : DiscoveryService
{
    private readonly IGitHubService _gitHubService;
    private readonly IMemoryCache _memoryCache;

    public GitHubDiscoveryService(
        ILogger<GitHubDiscoveryService> logger,
        IGitHubService gitHubService,
        IMemoryCache memoryCache) : base(logger)
    {
        _gitHubService = gitHubService;
        _memoryCache = memoryCache;
    }

    public override string Platform => "GitHub";

    protected override async Task StartAsync(CancellationToken cancellationToken)
    {
        var repositories = await _gitHubService.GetRepositoriesAsync(cancellationToken);

        var pullRequests = new List<GitHubPullRequest>();
        var pipelines = new List<GitHubPipeline>();

        foreach (var repository in repositories)
        {
            var repositoryPullRequests = await _gitHubService.GetPullRequestsAsync(repository);
            pullRequests.AddRange(repositoryPullRequests);

            var repositoryPipelines = await _gitHubService.GetPipelinesAsync(repository, cancellationToken);
            pipelines.AddRange(repositoryPipelines);
        }

        _memoryCache.Set(GitHubCacheConstants.RepositoryCacheKey, repositories);
        _memoryCache.Set(GitHubCacheConstants.PipelineCacheKey, pipelines);
        _memoryCache.Set(GitHubCacheConstants.PullRequestCacheKey, pullRequests);
    }
}