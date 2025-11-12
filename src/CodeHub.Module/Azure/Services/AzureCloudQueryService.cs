using CodeHub.Domain.Cloud;
using CodeHub.Domain.Cloud.Request;
using CodeHub.Domain.Cloud.Service;
using CodeHub.Module.Azure.Constants;
using CodeHub.Module.Azure.Models;
using CodeHub.Module.Shared.Extensions;
using CodeHub.Module.Shared.Query;
using CodeHub.Shared;
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
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Querying cloud resources from Azure");
        var azureCloudResources =
            _memoryCache.Get<List<AzureCloudResource>>(AzureCacheConstants.CloudResourceCacheKey) ?? [];
        var cloudResources = azureCloudResources.ConvertAll<CloudResource>(p => p);

        if (azureCloudResources.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<CloudResource>(cloudResources)
            .Where(request.Id, p => p.Id.Value == request.Id)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Description, p => p.Description.Contains(request.Description ?? string.Empty))
            .Where(request.Url, p => p.Url.ToString().Contains(request.Url ?? string.Empty))
            .Where(request.Type, p => p.Type.EqualsCaseInsensitive(request.Type))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }

    public List<CloudSecret> QueryCloudSecrets(CloudSecretQueryRequest request)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Querying cloud secrets from Azure");
        var azureCloudSecrets =
            _memoryCache.Get<List<CloudSecret>>(AzureCacheConstants.CloudSecretCacheKey) ?? [];
        var cloudSecrets = azureCloudSecrets.ConvertAll<CloudSecret>(p => p);

        if (azureCloudSecrets.Count <= 0)
        {
            return [];
        }

        return new QueryBuilder<CloudSecret>(cloudSecrets)
            .Where(request.Name, p => p.Name.Contains(request.Name ?? string.Empty))
            .Where(request.Location, p => p.Location.Contains(request.Location ?? string.Empty))
            .Where(request.Url, p => p.Url.ToString().Contains(request.Url ?? string.Empty))
            .Where(request.Platform, p => p.Platform == request.Platform)
            .ToList();
    }
}