using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Engine.AzureDevOps.Models;

public sealed record AzureDevOpsProject
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }

    public static AzureDevOpsProject MapFromTeamProject(TeamProject teamProject)
    {
        return new AzureDevOpsProject
        {
            Name = teamProject.Name,
            Description = teamProject.Description,
            Url = teamProject.Description,
        };
    }
}