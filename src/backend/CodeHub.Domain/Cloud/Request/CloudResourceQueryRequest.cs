using CodeHub.Domain.Shared;

namespace CodeHub.Domain.Cloud.Request;

public sealed record CloudResourceQueryRequest(
    string? Id,
    string? Name,
    string? Description,
    string? Url,
    string? Type,
    CloudPlatform? Platform) : BaseRequest;