namespace CodeHub.Core.Models;

public enum PipelinePlatform
{
    AzureDevOps,
    GitHubActions,
    GitLab,
    Jenkins,
    TravisCi
}

public abstract record PipelineResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required PipelinePlatform Platform { get; set; }
}