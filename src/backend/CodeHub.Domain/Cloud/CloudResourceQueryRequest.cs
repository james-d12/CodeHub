namespace CodeHub.Domain.Cloud;

public sealed record CloudResourceQueryRequest(string? Id, string? Name, CloudPlatform? Platform);