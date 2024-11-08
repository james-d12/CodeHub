﻿using CodeHub.Core.Models;

namespace CodeHub.Core.Platforms.AzureDevOps;

public sealed record AzureDevOpsRepository : GitRepositoryResource
{
    public required string Project { get; init; }
    public required string ProjectUrl { get; init; }
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }
}