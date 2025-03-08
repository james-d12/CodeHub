namespace CodeHub.Domain.Git;

public sealed record PipelineQueryRequest(string? Id, string? Name, string? Url, PipelinePlatform? Platform);