namespace CodeHub.Domain.Ticketing;

public readonly record struct WorkItemId(string Value);

public enum WorkItemPlatform
{
    AzureDevOps,
    Jira
}

public record WorkItem
{
    public required WorkItemId Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Type { get; init; }
    public required string State { get; init; }
    public required WorkItemPlatform Platform { get; init; }
}