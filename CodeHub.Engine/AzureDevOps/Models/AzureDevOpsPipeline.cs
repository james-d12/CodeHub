using Microsoft.TeamFoundation.Build.WebApi;

namespace CodeHub.Engine.AzureDevOps.Models;

public sealed record AzureDevOpsPipeline
{
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string Project { get; init; }
    public required string ProjectName { get; init; }
    public required string Path { get; init; }

    public static AzureDevOpsPipeline MapFromBuildDefinitionReference(BuildDefinitionReference buildDefinitionReference)
    {
        return new AzureDevOpsPipeline
        {
            Name = buildDefinitionReference.Name,
            Url = buildDefinitionReference.Url,
            Project = buildDefinitionReference.Project.Name,
            ProjectName = buildDefinitionReference.Project.Url,
            Path = buildDefinitionReference.Path
        };
    }
}