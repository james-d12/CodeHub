namespace CodeHub.Core.Shared.Services;

public interface IDiscoveryService
{
    string Platform { get; }
    Task DiscoveryAsync(CancellationToken cancellationToken);
}