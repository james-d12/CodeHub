using CodeHub.Domain.Cloud;
using CodeHub.Domain.Cloud.Request;
using CodeHub.Domain.Cloud.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("cloud")]
public sealed class CloudController : ControllerBase
{
    private readonly ILogger<CloudController> _logger;
    private readonly IEnumerable<ICloudQueryService> _cloudQueryServices;

    public CloudController(
        ILogger<CloudController> logger,
        IEnumerable<ICloudQueryService> cloudQueryServices)
    {
        _logger = logger;
        _cloudQueryServices = cloudQueryServices;
    }

    [HttpGet, Route("resources")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<CloudResource> GetCloudResources([FromQuery] CloudResourceQueryRequest request)
    {
        _logger.LogInformation("Querying cloud resources");
        var cloudResources = new List<CloudResource>();
        foreach (var queryService in _cloudQueryServices)
        {
            cloudResources.AddRange(queryService.QueryCloudResources(request));
        }

        return cloudResources;
    }

    [HttpGet, Route("secrets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<CloudSecret> GetCloudSecrets([FromQuery] CloudSecretQueryRequest request)
    {
        _logger.LogInformation("Querying cloud secrets");
        var cloudSecrets = new List<CloudSecret>();
        foreach (var queryService in _cloudQueryServices)
        {
            cloudSecrets.AddRange(queryService.QueryCloudSecrets(request));
        }

        return cloudSecrets;
    }
}