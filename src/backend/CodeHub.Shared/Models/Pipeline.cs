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
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required Uri Url { get; set; }
    public required PipelinePlatform Platform { get; set; }
}