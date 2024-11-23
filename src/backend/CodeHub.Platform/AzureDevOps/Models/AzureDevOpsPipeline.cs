using CodeHub.Core.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : PipelineResource
{
    public required string Project { get; init; }
    public required string ProjectName { get; init; }
    public required string Path { get; init; }
}