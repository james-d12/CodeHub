using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Platform.AzureDevOps.Mappers;

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