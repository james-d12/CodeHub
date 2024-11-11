namespace CodeHub.Core.Models.Resource;

public enum TicketingPlatform
{
    Jira,
    AzureDevOps
}

public abstract record TicketingResource
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required TicketingPlatform TicketingPlatform { get; init; }
}