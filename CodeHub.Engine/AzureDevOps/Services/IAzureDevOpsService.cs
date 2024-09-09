using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Engine.AzureDevOps.Services;

public interface IAzureDevOpsService
{
    Task<List<GitRepository>> GetRepositoriesAsync(string projectName, CancellationToken cancellationToken);
    Task<List<BuildDefinitionReference>> GetPipelinesAsync(string projectName, CancellationToken cancellationToken);
    Task<List<TeamProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<WebApiTeam>> GetTeamsAsync(CancellationToken cancellationToken);
}