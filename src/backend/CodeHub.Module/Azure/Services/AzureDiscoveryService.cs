using CodeHub.Domain.Discovery;
using Microsoft.Extensions.Logging;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureDiscoveryService : DiscoveryService
{
    private readonly ILogger<AzureDiscoveryService> _logger;
    private readonly IAzureService _azureService;

    public AzureDiscoveryService(
        ILogger<AzureDiscoveryService> logger,
        IAzureService azureService) : base(logger)
    {
        _logger = logger;
        _azureService = azureService;
    }

    public override string Platform => "Azure";

    protected override async Task StartAsync(CancellationToken cancellationToken)
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
}