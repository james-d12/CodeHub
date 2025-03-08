using CodeHub.Core.GitHub.Constants;
using CodeHub.Core.GitHub.Models;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Core.GitHub.Services;

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

        foreach (var repository in repositories)
        {
            var repositoryPullRequests = await _gitHubService.GetPullRequestsAsync(repository);
            pullRequests.AddRange(repositoryPullRequests);
        }

        _memoryCache.Set(CacheConstants.RepositoryCacheKey, repositories);
        _memoryCache.Set(CacheConstants.PullRequestCacheKey, pullRequests);
    }
}