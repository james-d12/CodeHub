namespace CodeHub.Domain.Git.Request;

public sealed record PullRequestQueryRequest(string? Id, string? Title, PullRequestPlatform? Platform);