using CodeHub.Engine.SonarCloud.Models;
using CodeHub.Engine.SonarCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("sonarcloud/")]
public sealed class SonarCloudController(ILogger<AzureDevOpsController> logger, ISonarCloudService sonarCloudService)
    : ControllerBase
{
    [HttpGet, Route("components")]
    public async Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Projects in Azure DevOps.");
        return await sonarCloudService.GetComponentsAsync(cancellationToken);
    }
}