using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.Shared.Query.Requests;

public sealed record RepositoryQueryRequest(string? Id, string? Name, string? Url, string? DefaultBranch, RepositoryPlatform? Platform);