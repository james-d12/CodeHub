using CodeHub.Core.Platforms.AzureDevOps;
using CodeHub.Core.Platforms.AzureDevOps.Models;
using CodeHub.Core.Platforms.AzureDevOps.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("azure-devops/")]
public sealed class AzureDevOpsController(ILogger<AzureDevOpsController> logger, IAzureDevOpsService azureDevOpsService)
    : ControllerBase
{
    [HttpGet, Route("{projectName}/repositories")]
    public async Task<List<AzureDevOpsRepository>> GetRepositoriesForProjectAsync(string projectName,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Repositories for {Project} in Azure DevOps.", projectName);
        return await azureDevOpsService.GetRepositoriesAsync(projectName, cancellationToken);
    }

    [HttpGet, Route("{projectName}/pipelines")]
    public async Task<List<AzureDevOpsPipeline>> GetPipelinesForProjectAsync(string projectName,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Pipelines for {Project} in Azure DevOps.", projectName);
        return await azureDevOpsService.GetPipelinesAsync(projectName, cancellationToken);
    }

    [HttpGet, Route("projects")]
    public async Task<List<AzureDevOpsProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Projects in Azure DevOps.");
        return await azureDevOpsService.GetProjectsAsync(cancellationToken);
    }
}