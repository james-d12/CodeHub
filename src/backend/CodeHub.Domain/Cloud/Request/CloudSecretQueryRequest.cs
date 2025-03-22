namespace CodeHub.Domain.Cloud.Request;

public sealed record CloudSecretQueryRequest(string? Name, string? Location, CloudSecretPlatform? Platform);