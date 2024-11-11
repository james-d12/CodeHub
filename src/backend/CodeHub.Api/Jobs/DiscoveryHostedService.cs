using CodeHub.Core.Services;

namespace CodeHub.Api.Jobs;

public sealed class DiscoveryHostedService : BackgroundService
{
    private readonly ILogger<DiscoveryHostedService> _logger;
    private readonly IDiscoveryService _discoveryService;

    public DiscoveryHostedService(ILogger<DiscoveryHostedService> logger, IDiscoveryService discoveryService)
    {
        _logger = logger;
        _discoveryService = discoveryService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await _discoveryService.DiscoverAsync(stoppingToken);
    }
}