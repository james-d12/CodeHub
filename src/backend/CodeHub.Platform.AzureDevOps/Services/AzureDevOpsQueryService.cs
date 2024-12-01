using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Models.Requests;
using CodeHub.Shared.Models;
using CodeHub.Shared.Models.Requests;
using CodeHub.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.TeamFoundation.Common;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsQueryService : IAzureDevOpsQueryService, IQueryService
{
    private readonly ILogger<AzureDevOpsQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsQueryService(ILogger<AzureDevOpsQueryService> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<AzureDevOpsPipeline> QueryPipelines(AzureDevOpsQueryPipelineRequest request)
    {
        _logger.LogInformation("Querying pipelines with Request: {Request}", request);
        var pipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];

        if (pipelines.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<AzureDevOpsPipeline>(pipelines)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.ProjectId, p => p.ProjectId == request.ProjectId)
            .Where(request.ProjectName, p => p.ProjectName.Contains(request.ProjectName ?? string.Empty))
            .ToList();
    }

    public List<Pipeline> QueryPipelines(QueryPipelineRequest request)
    {
        var azureDevOpsPipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
        var pipelines = azureDevOpsPipelines.ConvertAll(p => (Pipeline)p);

        if (azureDevOpsPipelines.IsNullOrEmpty())
        {
            return [];
        }

        return new QueryBuilder<Pipeline>(pipelines)
            .Where(request.Id, p => p.Id == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Url, p => p.Url == new Uri(request.Url ?? string.Empty))
            .ToList();
    }

    public List<Repository> QueryRepositories(QueryRepositoryRequest request)
    {
        throw new NotImplementedException();
    }
}