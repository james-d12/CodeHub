using CodeHub.Domain.Discovery;
using CodeHub.Shared;

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
        using var activity = Tracing.StartActivity();
        _logger.LogDebug("Worker running at: {time}", DateTimeOffset.Now);

        foreach (var discoveryService in _discoveryServices)
        {
            await discoveryService.DiscoveryAsync(stoppingToken);
        }

        _logger.LogDebug("Worker finished running at: {time}", DateTimeOffset.Now);
    }
}