namespace CodeHub.Domain.Ticketing.Request;

public sealed record WorkItemQueryRequest(
    string? Id,
    string? Title);