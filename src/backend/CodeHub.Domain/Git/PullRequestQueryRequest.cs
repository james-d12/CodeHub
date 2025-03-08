namespace CodeHub.Domain.Git;

public sealed record PullRequestQueryRequest(string? Id, string? Title, PullRequestPlatform? Platform);