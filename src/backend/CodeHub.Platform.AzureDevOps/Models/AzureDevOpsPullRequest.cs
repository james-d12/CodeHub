using System.Collections.Immutable;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsPullRequest
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required ImmutableHashSet<string> Labels { get; init; }
    public required ImmutableHashSet<string> Reviewers { get; init; }
    public required AzureDevOpsPullRequestStatus Status { get; init; }
}