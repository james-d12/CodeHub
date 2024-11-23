using CodeHub.Platform.Soos.Models;
using CodeHub.Platform.Soos.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("soos/")]
public sealed class SoosController(ILogger<AzureDevOpsController> logger, ISoosService soosService)
    : ControllerBase
{
    [HttpGet, Route("projects")]
    public async Task<List<SoosProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Projects in SooS.");
        return await soosService.GetProjectsAsync(cancellationToken);
    }

    [HttpGet, Route("projects/{projectId}/branches")]
    public async Task<List<SoosProjectBranch>> GetProjectsAsync(string projectId,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Branches for Project: {ProjectId} in SooS.", projectId);
        return await soosService.GetProjectBranchesAsync(projectId, cancellationToken);
    }

    [HttpGet, Route("projects/{projectId}/settings")]
    public async Task<SoosProjectSetting?> GetProjectSettingsAsync(string projectId,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Settings for Project: {ProjectId} in SooS.", projectId);
        return await soosService.GetProjectSettingsAsync(projectId, cancellationToken);
    }

    [HttpGet, Route("projects/{projectId}/vulnerabilities")]
    public async Task<List<SoosProjectBranchVulnerabilityResults>> GetProjectVulnerabilitiesAsync(string projectId,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Settings for Project: {ProjectId} in SooS.", projectId);
        return await soosService.GetProjectVulnerabilitiesAsync(projectId, cancellationToken);
    }
}