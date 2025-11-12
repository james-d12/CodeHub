using CodeHub.Domain.Shared;

namespace CodeHub.Domain.Cloud.Request;

public sealed record CloudSecretQueryRequest(
    string? Name,
    string? Location,
    string? Url,
    CloudSecretPlatform? Platform) : BaseRequest;