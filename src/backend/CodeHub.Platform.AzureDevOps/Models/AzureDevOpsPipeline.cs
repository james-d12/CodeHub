﻿using CodeHub.Shared.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : Pipeline
{
    public required string ProjectName { get; init; }
    public required Guid ProjectId { get; init; }
    public required string ProjectUrl { get; init; }
    public required string Path { get; init; }
}