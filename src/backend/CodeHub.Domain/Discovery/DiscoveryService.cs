using System.Diagnostics;
using CodeHub.Shared;
using Microsoft.Extensions.Logging;

namespace CodeHub.Domain.Discovery;

public abstract class DiscoveryService : IDiscoveryService
{
    private readonly ILogger<DiscoveryService> _logger;

    protected DiscoveryService(ILogger<DiscoveryService> logger)
    {
        _logger = logger;
    }

    public virtual string Platform => string.Empty;

    public async Task DiscoveryAsync(CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        try
        {
            _logger.LogInformation("{Platform} Discovery Service started.", Platform);
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await StartAsync(cancellationToken);

            stopWatch.Stop();
            var milliseconds = stopWatch.Elapsed.TotalMilliseconds;

            _logger.LogInformation("{Platform} Discovery Service took: {Milliseconds} ms", Platform, milliseconds);
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Error occurred whilst trying to discover the latest {Platform} resources.",
                Platform);
            throw;
        }
    }

    protected abstract Task StartAsync(CancellationToken cancellationToken);
}