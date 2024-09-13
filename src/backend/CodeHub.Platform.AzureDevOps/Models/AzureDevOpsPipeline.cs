namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline
{
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string Project { get; init; }
    public required string ProjectName { get; init; }
    public required string Path { get; init; }
}