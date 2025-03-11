namespace CodeHub.Domain.Git.Request;

public sealed record PullRequestQueryRequest(
    string? Id,
    string? Name,
    string? Description,
    string? Url,
    PullRequestPlatform? Platform);