namespace CodeHub.Core.Platforms.AzureDevOps;

public sealed record AzureDevOpsRepository
{
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required string Project { get; init; }
    public required string ProjectUrl { get; init; }
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}