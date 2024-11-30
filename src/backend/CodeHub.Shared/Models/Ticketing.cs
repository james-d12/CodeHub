namespace CodeHub.Shared.Models;

public enum TicketingPlatform
{
    Jira,
    AzureDevOps
}

public abstract record Ticketing
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required TicketingPlatform TicketingPlatform { get; init; }
}