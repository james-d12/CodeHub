using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Core.Platforms.AzureDevOps;

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