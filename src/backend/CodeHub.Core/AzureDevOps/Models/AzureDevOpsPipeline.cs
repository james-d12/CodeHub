using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : Pipeline
{
    public required string Path { get; init; }
}