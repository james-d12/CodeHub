using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.Shared.Query.Requests;

public sealed record PullRequestQueryRequest(string? Id, string? Title, PullRequestPlatform? Platform);