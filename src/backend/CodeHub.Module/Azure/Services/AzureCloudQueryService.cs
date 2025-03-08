using CodeHub.Domain.Cloud;
using CodeHub.Module.Azure.Constants;
using CodeHub.Module.Azure.Models;
using CodeHub.Module.Shared.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureCloudQueryService : ICloudQueryService
{
    private readonly ILogger<AzureCloudQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureCloudQueryService(
        ILogger<AzureCloudQueryService> logger,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<CloudResource> QueryCloudResources(CloudResourceQueryRequest request)
    {
        _logger.LogInformation("Querying cloud resources from Azure");
        var azureCloudResources = _memoryCache.Get<List<AzureCloudResource>>(CacheConstants.CloudResourceCacheKey) ?? [];
        var cloudResources = azureCloudResources.ConvertAll<CloudResource>(p => p);

        if (azureCloudResources.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<CloudResource>(cloudResources)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}