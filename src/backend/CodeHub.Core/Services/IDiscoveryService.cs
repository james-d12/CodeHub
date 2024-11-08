namespace CodeHub.Core.Services;

public interface IDiscoveryService
{
    Task DiscoverAsync(CancellationToken cancellationToken);
}