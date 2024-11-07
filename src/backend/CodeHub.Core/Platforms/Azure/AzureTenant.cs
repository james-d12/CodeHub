using Azure.Core;

namespace CodeHub.Core.Platforms.Azure;

public sealed record AzureTenant
{
    public required ResourceIdentifier ResourceIdentifier { get; init; }
    public required Guid Id { get; init; }
    public required string FullyQualifiedId { get; init; }
    public required string Name { get; init; }
    public required string DefaultDomain { get; init; }
    public required string Country { get; init; }
    public required IReadOnlyList<string> Domains { get; init; }
    public required IReadOnlyList<AzureSubscription> Subscriptions { get; init; }
}