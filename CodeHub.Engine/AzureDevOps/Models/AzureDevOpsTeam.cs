using Microsoft.TeamFoundation.Core.WebApi;

namespace CodeHub.Engine.AzureDevOps.Models;

public sealed record AzureDevOpsTeam
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }

    public static AzureDevOpsTeam MapFromWebApiTeam(WebApiTeam webApiTeam)
    {
        return new AzureDevOpsTeam
        {
            Name = webApiTeam.Name,
            Description = webApiTeam.Description,
            Url = webApiTeam.Url
        };
    }
}