using CodeHub.Domain.Git;

namespace CodeHub.Module.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline : Pipeline
{
    public required string Path { get; init; }
}