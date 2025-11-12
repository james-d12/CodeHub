using CodeHub.Domain.Shared;

namespace CodeHub.Domain.Git.Request;

public sealed record PipelineQueryRequest(
    string? Id,
    string? Name,
    string? Url,
    string? OwnerName,
    PipelinePlatform? Platform) : BaseRequest;