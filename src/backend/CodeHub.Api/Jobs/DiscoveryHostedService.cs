using CodeHub.Core.Services;

namespace CodeHub.Api.Jobs;

public sealed class DiscoveryHostedService : BackgroundService
{
    private readonly ILogger<DiscoveryHostedService> _logger;
    private readonly IEnumerable<IDiscoveryService> _discoveryServices;

    public DiscoveryHostedService(
        ILogger<DiscoveryHostedService> logger,
        IEnumerable<IDiscoveryService> discoveryServices)
    {
        _logger = logger;
        _discoveryServices = discoveryServices;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        foreach (var discoveryService in _discoveryServices)
        {
            await discoveryService.DiscoverAsync(stoppingToken);
        }
    }
}