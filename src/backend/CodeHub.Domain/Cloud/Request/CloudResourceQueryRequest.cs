namespace CodeHub.Domain.Cloud.Request;

public sealed record CloudResourceQueryRequest(string? Id, string? Name, CloudPlatform? Platform);