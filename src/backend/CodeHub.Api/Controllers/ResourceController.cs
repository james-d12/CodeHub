using CodeHub.Shared.Models;
using CodeHub.Shared.Models.Requests;
using CodeHub.Shared.Query;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("resources/")]
public sealed class ResourceDevOpsController : ControllerBase
{
    private readonly IEnumerable<IQueryService> _queryServices;

    public ResourceDevOpsController(IEnumerable<IQueryService> queryServices)
    {
        _queryServices = queryServices;
    }

    [HttpGet, Route("pipelines")]
    public List<Pipeline> GetPipelines([FromQuery] QueryPipelineRequest request)
    {
        var pipelines = new List<Pipeline>();
        foreach (var queryService in _queryServices)
        {
            pipelines.AddRange(queryService.QueryPipelines(request));
        }

        return pipelines;
    }
}