using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Models.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.TeamFoundation.Common;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsQueryService : IAzureDevOpsQueryService
{
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsQueryService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<AzureDevOpsPipeline> QueryPipelines(AzureDevOpsQueryPipelineRequest request)
    {
        var pipelines = _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];

        if (pipelines.IsNullOrEmpty())
        {
            return [];
        }

        var pipelineQuery = pipelines.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Id))
        {
            pipelineQuery = pipelineQuery.Where(p => p.Id == request.Id);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            pipelineQuery = pipelineQuery.Where(p => p.Name.Contains(request.Name));
        }

        return pipelineQuery.ToList();
    }
}