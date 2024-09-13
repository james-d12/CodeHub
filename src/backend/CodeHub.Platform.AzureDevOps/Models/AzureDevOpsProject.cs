namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsProject
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }
}