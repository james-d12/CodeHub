﻿namespace CodeHub.Domain.Git;

public enum PipelinePlatform
{
    AzureDevOps,
    GitHub,
    GitLab,
    Jenkins,
    TravisCi
}

public readonly record struct PipelineId(string Value);

public record Pipeline
{
    public required PipelineId Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required Owner Owner { get; init; }
    public required PipelinePlatform Platform { get; init; }
}