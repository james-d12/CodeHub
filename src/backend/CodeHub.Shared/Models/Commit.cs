namespace CodeHub.Shared.Models;

public record Commit
{
    public required string Id { get; init; }
    public required Uri Url { get; init; }
    public required string Committer { get; init; }
    public required string? Comment { get; init; }
    public required int? ChangeCount { get; init; }
}