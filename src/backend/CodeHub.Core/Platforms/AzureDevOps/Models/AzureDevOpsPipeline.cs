using CodeHub.Core.Models.Resource;

namespace CodeHub.Core.Platforms.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : PipelineResource
{
    public required string Project { get; init; }
    public required string ProjectName { get; init; }
    public required string Path { get; init; }
}