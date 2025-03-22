using CodeHub.Domain.Ticketing;

namespace CodeHub.Portal.Services.Services;

public interface ITicketingClient
{
    Task<List<WorkItem>> GetWorkItemsAsync();
}