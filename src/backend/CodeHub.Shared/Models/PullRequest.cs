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

public record PullRequest
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required ImmutableHashSet<string> Labels { get; init; }
    public required ImmutableHashSet<string> Reviewers { get; init; }
    public required PullRequestStatus Status { get; init; }
    public required PullRequestPlatform Platform { get; init; }
}