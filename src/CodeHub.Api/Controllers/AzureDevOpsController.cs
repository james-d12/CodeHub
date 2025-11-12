using CodeHub.Module.AzureDevOps.Services;
using CodeHub.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("azure-devops")]
public sealed class AzureDevOpsController : ControllerBase
{
    private readonly ILogger<CloudController> _logger;
    private readonly IAzureDevOpsQueryService _azureDevOpsQueryService;

    public AzureDevOpsController(ILogger<CloudController> logger, IAzureDevOpsQueryService azureDevOpsQueryService)
    {
        _azureDevOpsQueryService = azureDevOpsQueryService;
        _logger = logger;
    }

    [HttpGet, Route("repositories/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAzureDevOpsRepository(string name)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Getting Azure DevOps Repository with {Name}", name);
        var repository = _azureDevOpsQueryService.GetRepository(name);
        return repository is not null ? Ok(repository) : NotFound();
    }

    [HttpGet, Route("pipelines/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAzureDevOpsPipeline(string name)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Getting Azure DevOps Pipeline with {Name}", name);
        var pipeline = _azureDevOpsQueryService.GetPipeline(name);
        return pipeline is not null ? Ok(pipeline) : NotFound();
    }
}