namespace CodeHub.Domain.Git.Request;

public sealed record RepositoryQueryRequest(string? Id, string? Name, string? Url, string? DefaultBranch, RepositoryPlatform? Platform);