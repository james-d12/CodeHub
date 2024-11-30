using CodeHub.Platform.AzureDevOps.Models;
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
    public List<AzureDevOpsPipeline> GetPipelines()
    {
        return _azureDevOpsQueryService.QueryPipelines();
    }
}