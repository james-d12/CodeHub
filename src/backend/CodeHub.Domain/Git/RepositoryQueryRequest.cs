namespace CodeHub.Domain.Git;

public sealed record RepositoryQueryRequest(string? Id, string? Name, string? Url, string? DefaultBranch, RepositoryPlatform? Platform);