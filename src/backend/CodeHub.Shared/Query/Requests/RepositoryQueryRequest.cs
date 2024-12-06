using CodeHub.Shared.Models;

namespace CodeHub.Shared.Query.Requests;

public sealed record RepositoryQueryRequest(string? Id, string? Name, string? Url, string? DefaultBranch, RepositoryPlatform? Platform);