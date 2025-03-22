using CodeHub.Domain.Ticketing;
using CodeHub.Domain.Ticketing.Request;
using CodeHub.Domain.Ticketing.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("ticketing/")]
public sealed class TicketingController : ControllerBase
{
    private readonly IEnumerable<ITicketingQueryService> _queryServices;

    public TicketingController(IEnumerable<ITicketingQueryService> queryServices)
    {
        _queryServices = queryServices;
    }

    [HttpGet, Route("work-items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<WorkItem> GetPipelines([FromQuery] WorkItemQueryRequest request)
    {
        var workItems = new List<WorkItem>();
        foreach (var queryService in _queryServices)
        {
            workItems.AddRange(queryService.QueryWorkItems(request));
        }

        return workItems;
    }
}