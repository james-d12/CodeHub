using CodeHub.Domain.Shared;

namespace CodeHub.Domain.Git.Request;

public sealed record RepositoryQueryRequest(
    string? Id,
    string? Name,
    string? Url,
    string? DefaultBranch,
    string? OwnerName,
    RepositoryPlatform? Platform) : BaseRequest;