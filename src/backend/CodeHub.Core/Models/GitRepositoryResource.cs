namespace CodeHub.Core.Models;

public enum GitPlatform
{
    AzureDevOps,
    GitHub,
    GitLab,
    None
}

public abstract record GitRepositoryResource
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required GitPlatform Platform { get; init; }
}