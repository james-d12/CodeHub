using CodeHub.Platform.GitHub.Constants;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubDiscoveryService : DiscoveryService
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
        _memoryCache.Set(CacheConstants.RepositoryCacheKey, repositories);
    }
}