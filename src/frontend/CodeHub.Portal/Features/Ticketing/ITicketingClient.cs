using CodeHub.Domain.Ticketing;

namespace CodeHub.Portal.Features.Ticketing;

public interface ITicketingClient
{
    Task<List<WorkItem>> GetWorkItemsAsync();
}