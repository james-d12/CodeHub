using CodeHub.Platform.GitHub.Constants;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubDiscoveryService : IDiscoveryService
{
    private readonly ILogger<GitHubDiscoveryService> _logger;
    private readonly IGitHubService _gitHubService;
    private readonly IMemoryCache _memoryCache;

    public GitHubDiscoveryService(ILogger<GitHubDiscoveryService> logger, IGitHubService gitHubService,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _gitHubService = gitHubService;
        _memoryCache = memoryCache;
    }

    public async Task DiscoverAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Discovering GitHub repositories...");

            var repositories = await _gitHubService.GetRepositoriesAsync(cancellationToken);

            _memoryCache.Set(CacheConstants.RepositoryCacheKey, repositories);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred whilst trying to discover the latest GitHub resources.");
        }
    }
}