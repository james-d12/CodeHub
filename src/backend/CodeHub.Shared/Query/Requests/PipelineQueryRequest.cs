using CodeHub.Shared.Models;

namespace CodeHub.Shared.Query.Requests;

public sealed record PipelineQueryRequest(string? Id, string? Name, string? Url, PipelinePlatform? Platform);