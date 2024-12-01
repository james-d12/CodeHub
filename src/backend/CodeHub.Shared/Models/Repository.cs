namespace CodeHub.Shared.Models;

public enum GitPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public abstract record Repository
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required GitPlatform Platform { get; init; }
}