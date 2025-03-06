using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.Shared.Query.Requests;

public sealed record PipelineQueryRequest(string? Id, string? Name, string? Url, PipelinePlatform? Platform);