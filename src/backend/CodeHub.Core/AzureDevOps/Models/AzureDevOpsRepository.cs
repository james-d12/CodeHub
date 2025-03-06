using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.AzureDevOps.Models;

public sealed record AzureDevOpsRepository : Repository
{
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}