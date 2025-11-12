using CodeHub.Domain.Ticketing;
using CodeHub.Domain.Ticketing.Request;
using CodeHub.Domain.Ticketing.Service;
using CodeHub.Module.AzureDevOps.Constants;
using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Module.Shared.Extensions;
using CodeHub.Module.Shared.Query;
using CodeHub.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.AzureDevOps.Services;

public sealed class AzureDevOpsTicketingQueryService : ITicketingQueryService
{
    private readonly ILogger<AzureDevOpsTicketingQueryService> _logger;
    private readonly IMemoryCache _memoryCache;

    public AzureDevOpsTicketingQueryService(
        ILogger<AzureDevOpsTicketingQueryService> logger,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public List<WorkItem> QueryWorkItems(WorkItemQueryRequest request)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Querying work items from Azure DevOps");
        var azureWorkItems = _memoryCache.Get<List<AzureDevOpsWorkItem>>(AzureDevOpsCacheConstants.WorkItemsCacheKey) ??
                             [];
        var workItems = azureWorkItems.ConvertAll<WorkItem>(p => p);

        return new QueryBuilder<WorkItem>(workItems)
            .Where(request.Id, p => p.Id.Value.EqualsCaseInsensitive(request.Id))
            .ToList();
    }
}