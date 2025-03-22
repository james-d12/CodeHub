using CodeHub.Domain.Ticketing.Request;

namespace CodeHub.Domain.Ticketing.Service;

public interface ITicketingQueryService
{
    List<WorkItem> QueryWorkItems(WorkItemQueryRequest request);
}