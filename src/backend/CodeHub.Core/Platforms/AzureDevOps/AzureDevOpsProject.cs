namespace CodeHub.Core.Platforms.AzureDevOps;

public sealed record AzureDevOpsProject
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Uri Url { get; init; }
}