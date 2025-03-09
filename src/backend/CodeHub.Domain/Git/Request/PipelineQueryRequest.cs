namespace CodeHub.Domain.Git.Request;

public sealed record PipelineQueryRequest(string? Id, string? Name, string? Url, PipelinePlatform? Platform);