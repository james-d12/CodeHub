using CodeHub.Shared.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsRepository : Repository
{
    public required string ProjectName { get; init; }
    public required Guid ProjectId { get; init; }
    public required string ProjectUrl { get; init; }
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}