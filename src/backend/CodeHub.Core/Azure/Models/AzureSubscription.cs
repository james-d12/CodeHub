namespace CodeHub.Core.Azure.Models;

public sealed record AzureSubscription
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required string Tenant { get; init; }
    public required Guid? TenantId { get; init; }
    public required IReadOnlyDictionary<string, string> Tags { get; init; }
}