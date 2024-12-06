using CodeHub.Shared.Models;

namespace CodeHub.Shared.Query.Requests;

public sealed record PullRequestQueryRequest(string? Id, string? Title, PullRequestPlatform? Platform);