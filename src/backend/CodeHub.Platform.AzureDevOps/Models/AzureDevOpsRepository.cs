using CodeHub.Shared.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsRepository : Repository
{
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}