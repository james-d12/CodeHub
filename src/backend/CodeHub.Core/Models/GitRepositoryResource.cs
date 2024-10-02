namespace CodeHub.Core.Models;

public enum GitPlatform
{
    AzureDevOps,
    GitHub,
    GitLab,
    None
}

public abstract class GitRepositoryResource
{
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required GitPlatform Platform { get; init; }
}