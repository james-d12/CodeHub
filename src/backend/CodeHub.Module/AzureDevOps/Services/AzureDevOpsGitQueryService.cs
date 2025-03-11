using CodeHub.Domain.Git;
using CodeHub.Domain.Git.Request;
using CodeHub.Domain.Git.Service;
using CodeHub.Module.AzureDevOps.Constants;
using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Module.Shared.Extensions;
using CodeHub.Module.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.AzureDevOps.Services;

public sealed class AzureDevOpsGitQueryService : IGitQueryService
{
    private readonly ILogger<AzureDevOpsGitQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsGitQueryService(ILogger<AzureDevOpsGitQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<Pipeline> QueryPipelines(PipelineQueryRequest request)
    {
        _logger.LogInformation("Querying pipelines from Azure DevOps");
        var azureDevOpsPipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = azureDevOpsPipelines.ConvertAll<Pipeline>(p => p);

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.OwnerName, p => p.Owner.Name.EqualsCaseInsensitive(request.OwnerName))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<Repository> QueryRepositories(RepositoryQueryRequest request)
    {
        _logger.LogInformation("Querying repositories from Azure DevOps");
        var azureDevOpsRepositories =
            _memoryCache.Get<List<AzureDevOpsRepository>>(CacheConstants.RepositoryCacheKey) ?? [];
        var repositories = azureDevOpsRepositories.ConvertAll<Repository>(p => p);

        return new QueryBuilder<Repository>(repositories)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.OwnerName, p => p.Owner.Name.EqualsCaseInsensitive(request.OwnerName))
            .Where(request.DefaultBranch, p => p.DefaultBranch.EqualsCaseInsensitive(request.DefaultBranch))
            .ToList();
    }

    public List<PullRequest> QueryPullRequests(PullRequestQueryRequest request)
    {
        _logger.LogInformation("Querying pull requests from Azure DevOps");
        var azureDevOpsPullRequests =
            _memoryCache.Get<List<AzureDevOpsPullRequest>>(CacheConstants.PullRequestCacheKey) ?? [];
        var pullRequests = azureDevOpsPullRequests.ConvertAll<PullRequest>(p => p);

        return new QueryBuilder<PullRequest>(pullRequests)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .Where(request.Name, p => p.Name.ContainsCaseInsensitive(request.Name))
            .Where(request.Description, p => p.Description.ContainsCaseInsensitive(request.Description))
            .Where(request.Url, p => p.Url.ToString().ContainsCaseInsensitive(request.Url))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}