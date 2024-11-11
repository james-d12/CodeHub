using CodeHub.Core.Services;
using Microsoft.Extensions.Logging;

namespace CodeHub.Core.Platforms.Azure;

public sealed class AzureDiscoveryService : IDiscoveryService
{
    private readonly ILogger<AzureDiscoveryService> _logger;
    private readonly IAzureService _azureService;

    public AzureDiscoveryService(ILogger<AzureDiscoveryService> logger, IAzureService azureService)
    {
        _logger = logger;
        _azureService = azureService;
    }

    public async Task DiscoverAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Discovering Azure resources...");
        try
        {
            _logger.LogInformation("Discovering Azure Tenant resources...");
            await _azureService.GetTenantsAsync(cancellationToken);

            _logger.LogInformation("Discovering Azure Subscription resources.");
            var subscriptions = await _azureService.GetSubscriptionsAsync(cancellationToken);

            foreach (var subscription in subscriptions)
            {
                await _azureService.GetSubscriptionResourcesAsync(subscription.Id, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred whilst trying to discover Azure resources.");
        }
    }
}