namespace CodeHub.Shared.Services;

public interface IDiscoveryService
{
    Task DiscoverAsync(CancellationToken cancellationToken);
}