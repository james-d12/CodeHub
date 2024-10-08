using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Core.Platforms.AzureDevOps;

internal static class AzureDevOpsTeamMapper
{
    internal static AzureDevOpsTeam MapFromWebApiTeam(WebApiTeam webApiTeam)
    {
        return new AzureDevOpsTeam
        {
            Name = webApiTeam.Name,
            Description = webApiTeam.Description,
            Url = webApiTeam.Url
        };
    }
}