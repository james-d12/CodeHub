using CodeHub.Core.Models.Resource;

namespace CodeHub.Core.Platforms.AzureDevOps.Models;

public sealed record AzureDevOpsRepository : RepositoryResource
{
    public required string Project { get; init; }
    public required string ProjectUrl { get; init; }
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}