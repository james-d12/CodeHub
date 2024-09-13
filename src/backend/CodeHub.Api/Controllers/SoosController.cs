using CodeHub.Platform.SooS.Models;
using CodeHub.Platform.SooS.Services;
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
}