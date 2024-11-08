﻿namespace CodeHub.Core.Platforms.AzureDevOps;

public sealed record AzureDevOpsTeam
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }
}