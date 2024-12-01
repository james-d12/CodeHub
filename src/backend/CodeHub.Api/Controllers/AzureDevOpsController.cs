using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Models.Requests;
using CodeHub.Platform.AzureDevOps.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("azure-devops/")]
public sealed class AzureDevOpsController : ControllerBase
{
    private readonly IAzureDevOpsQueryService _azureDevOpsQueryService;

    public AzureDevOpsController(IAzureDevOpsQueryService azureDevOpsQueryService)
    {
        _azureDevOpsQueryService = azureDevOpsQueryService;
    }

    [HttpGet, Route("pipelines")]
    public List<AzureDevOpsPipeline> GetPipelines([FromQuery] AzureDevOpsQueryPipelineRequest request)
    {
        return _azureDevOpsQueryService.QueryPipelines(request);
    }

    [HttpGet, Route("repositories")]
    public List<AzureDevOpsRepository> GetRepositories([FromQuery] AzureDevOpsQueryRepositoryRequest request)
    {
        return _azureDevOpsQueryService.QueryRepositories(request);
    }

    [HttpGet, Route("projects")]
    public List<AzureDevOpsProject> GetProjects()
    {
        return _azureDevOpsQueryService.QueryProjects();
    }

    [HttpGet, Route("projects/{projectName}/repositories")]
    public List<AzureDevOpsRepository> GetProjectRepositories([FromRoute] string projectName)
    {
        var request = new AzureDevOpsQueryRepositoryRequest { ProjectName = projectName };
        return _azureDevOpsQueryService.QueryRepositories(request);
    }

    [HttpGet, Route("teams")]
    public List<AzureDevOpsTeam> GetTeams()
    {
        return _azureDevOpsQueryService.QueryTeams();
    }

    [HttpGet, Route("pull-requests")]
    public List<AzureDevOpsPullRequest> GetPullRequests()
    {
        return _azureDevOpsQueryService.QueryPullRequests();
    }

    [HttpGet, Route("work-items")]
    public List<AzureDevOpsWorkItem> GetWorkItems()
    {
        return _azureDevOpsQueryService.QueryWorkItems();
    }
}