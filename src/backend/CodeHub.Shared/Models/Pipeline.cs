namespace CodeHub.Shared.Models;

public enum PipelinePlatform
{
    AzureDevOps,
    GitHubActions,
    GitLab,
    Jenkins,
    TravisCi
}

public record Pipeline
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required Uri Url { get; init; }
    public required Project Project { get; init; }
    public required PipelinePlatform Platform { get; init; }
}