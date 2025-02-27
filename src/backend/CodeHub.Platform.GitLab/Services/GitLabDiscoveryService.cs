using CodeHub.Shared.Services;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitLab.Services;

internal sealed class GitLabDiscoveryService : IDiscoveryService
{
    private readonly ILogger<GitLabDiscoveryService> _logger;
    private readonly IGitLabService _gitLabService;

    public GitLabDiscoveryService(
        ILogger<GitLabDiscoveryService> logger,
        IGitLabService gitLabService)
    {
        _logger = logger;
        _gitLabService = gitLabService;
    }

    public Task DiscoverAsync(CancellationToken cancellationToken)
    {
        try
        {
            _gitLabService.GetProjects();
            return Task.FromResult(true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred whilst trying to discover the latest GitHub resources.");
            return Task.FromResult(false);
        }
    }
}