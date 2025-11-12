using CodeHub.Domain.Discovery;
using CodeHub.Module.Azure.Constants;
using CodeHub.Module.Azure.Models;
using CodeHub.Shared;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureDiscoveryService : DiscoveryService
{
    private readonly ILogger<AzureDiscoveryService> _logger;
    private readonly IAzureService _azureService;
    private readonly AzureSettings _azureSettings;
    private readonly IMemoryCache _memoryCache;

    public AzureDiscoveryService(
        ILogger<AzureDiscoveryService> logger,
        IAzureService azureService,
        IOptions<AzureSettings> azureSettings,
        IMemoryCache memoryCache) : base(logger)
    {
        _logger = logger;
        _azureService = azureService;
        _azureSettings = azureSettings.Value;
        _memoryCache = memoryCache;
    }

    public override string Platform => "Azure";

    protected override async Task StartAsync(CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Discovering Azure Tenant resources...");
        var tenants = await _azureService.GetTenantsAsync(cancellationToken);

        _logger.LogInformation("Discovering Azure Subscription resources.");
        var subscriptions =
            await _azureService.GetSubscriptionsAsync(_azureSettings.SubscriptionFilters, cancellationToken);

        var cloudResources = new List<AzureCloudResource>();

        foreach (var subscription in subscriptions)
        {
            var tenantResource = tenants.Find(t => t.Data.TenantId == subscription.Data.TenantId);

            if (tenantResource is null)
            {
                continue;
            }

            var subscriptionResources =
                await _azureService.GetResourcesAsync(subscription, tenantResource, cancellationToken);
            cloudResources.AddRange(subscriptionResources);
        }

        _memoryCache.Set(AzureCacheConstants.CloudResourceCacheKey, cloudResources);
    }
}