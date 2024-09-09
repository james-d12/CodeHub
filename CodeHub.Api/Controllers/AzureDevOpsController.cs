using CodeHub.Engine.AzureDevOps.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("azure-devops/")]
public sealed class AzureDevOpsController(ILogger<AzureDevOpsController> logger, IAzureDevOpsService azureDevOpsService)
    : ControllerBase
{
    [HttpGet, Route("{projectName}/repositories")]
    public async Task<List<GitRepository>> GetRepositoriesForProjectAsync(string projectName,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Repositories for {Project} in Azure DevOps.", projectName);
        return await azureDevOpsService.GetRepositoriesAsync(projectName, cancellationToken);
    }

    [HttpGet, Route("{projectName}/pipelines")]
    public async Task<List<BuildDefinitionReference>> GetPipelinesForProjectAsync(string projectName,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Pipelines for {Project} in Azure DevOps.", projectName);
        return await azureDevOpsService.GetPipelinesAsync(projectName, cancellationToken);
    }

    [HttpGet, Route("projects")]
    public async Task<List<TeamProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Projects in Azure DevOps.");
        return await azureDevOpsService.GetProjectsAsync(cancellationToken);
    }
}