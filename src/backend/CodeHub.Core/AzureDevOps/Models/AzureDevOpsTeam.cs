namespace CodeHub.Core.AzureDevOps.Models;

public sealed record AzureDevOpsTeam
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }
}