﻿namespace CodeHub.Domain.Git;

public enum OwnerPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public readonly record struct OwnerId(string Value);

public record Owner
{
    public required OwnerId Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required Uri Url { get; init; }
    public required OwnerPlatform Platform { get; init; }
}