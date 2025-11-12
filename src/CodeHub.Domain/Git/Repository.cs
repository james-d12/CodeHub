namespace CodeHub.Domain.Git;

public enum RepositoryPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public readonly record struct RepositoryId(string Value);

public record Repository
{
    public required RepositoryId Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required Owner Owner { get; init; }
    public required RepositoryPlatform Platform { get; init; }
}