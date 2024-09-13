using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Platform.AzureDevOps.Mappers;

internal static class AzureDevOpsProjectMapper
{
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