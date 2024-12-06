using CodeHub.Shared.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : Pipeline
{
    public required string Path { get; init; }
}