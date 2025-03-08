namespace CodeHub.Domain.Git;

public readonly record struct CommitId(string Value);

public record Commit
{
    public required CommitId Id { get; init; }
    public required Uri Url { get; init; }
    public required string Committer { get; init; }
    public required string? Comment { get; init; }
    public required int? ChangeCount { get; init; }
}