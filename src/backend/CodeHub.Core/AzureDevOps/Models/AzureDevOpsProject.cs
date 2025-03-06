namespace CodeHub.Core.AzureDevOps.Models;

public sealed record AzureDevOpsProject
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Uri Url { get; init; }
}