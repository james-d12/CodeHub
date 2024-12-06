namespace CodeHub.Shared.Models;

public enum RepositoryPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public record Repository
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required Project Project { get; init; }
    public required RepositoryPlatform Platform { get; init; }
}