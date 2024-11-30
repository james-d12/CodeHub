using CodeHub.Platform.AzureDevOps.Constants;
using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsQueryService : IAzureDevOpsQueryService
{
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsQueryService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<AzureDevOpsPipeline> QueryPipelines()
    {
        return _memoryCache.Get<List<AzureDevOpsPipeline>>(CacheConstants.PipelineCacheKey) ?? [];
    }
}