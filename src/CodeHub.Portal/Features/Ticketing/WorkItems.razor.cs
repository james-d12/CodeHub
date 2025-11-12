using CodeHub.Domain.Ticketing;
using CodeHub.Portal.Features.Shared;

namespace CodeHub.Portal.Features.Ticketing;

public partial class WorkItems
{
    private readonly ILogger<WorkItems> _logger;
    private readonly ITicketingClient _ticketingClient;
    private List<WorkItem>? _workItems;

    public WorkItems(ILogger<WorkItems> logger, ITicketingClient ticketingClient)
    {
        _logger = logger;
        _ticketingClient = ticketingClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Work Items component");
        _workItems = await _ticketingClient.GetWorkItemsAsync();
    }

    private static string GetPlatformIcon(WorkItem workItem)
    {
        return workItem.Platform switch
        {
            WorkItemPlatform.AzureDevOps => PlatformIcons.AzureDevOps,
            _ => string.Empty
        };
    }
}