using System.Collections.Immutable;

namespace CodeHub.Shared.Models;

public enum PullRequestPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public enum PullRequestStatus
{
    Draft,
    Active,
    Completed,
    Abandoned,
    Unknown
}

public readonly record struct PullRequestId(string Value);

public record PullRequest
{
    public required PullRequestId Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required Uri Url { get; init; }
    public required ImmutableHashSet<string> Labels { get; init; }
    public required ImmutableHashSet<string> Reviewers { get; init; }
    public required PullRequestStatus Status { get; init; }
    public required PullRequestPlatform Platform { get; init; }
    public required Commit LastCommit { get; init; }
    public required Uri RepositoryUrl { get; init; }
    public required string RepositoryName { get; init; }
    public required DateOnly CreatedOnDate { get; init; }
}