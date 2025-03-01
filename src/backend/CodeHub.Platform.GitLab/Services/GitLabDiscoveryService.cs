using CodeHub.Platform.GitLab.Constants;
using CodeHub.Platform.GitLab.Models;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Platform.GitLab.Services;

internal sealed class GitLabDiscoveryService : IDiscoveryService
{
    private readonly ILogger<GitLabDiscoveryService> _logger;
    private readonly IGitLabService _gitLabService;
    private readonly IMemoryCache _memoryCache;

    public GitLabDiscoveryService(
        ILogger<GitLabDiscoveryService> logger,
        IGitLabService gitLabService,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _gitLabService = gitLabService;
        _memoryCache = memoryCache;
    }

    public Task DiscoverAsync(CancellationToken cancellationToken)
    {
        try
        {
            var projects = _gitLabService.GetProjects();
            var pullRequests = _gitLabService.GetPullRequests();

            var pipelines = new List<GitLabPipeline>();
            foreach (var project in projects)
            {
                var projectPipelines = _gitLabService.GetPipelines(project);
                pipelines.AddRange(projectPipelines);
            }

            _memoryCache.Set(CacheConstants.PipelineCacheKey, pipelines);
            _memoryCache.Set(CacheConstants.PullRequestCacheKey, pullRequests);

            return Task.FromResult(true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred whilst trying to discover the latest GitHub resources.");
            return Task.FromResult(false);
        }
    }
}