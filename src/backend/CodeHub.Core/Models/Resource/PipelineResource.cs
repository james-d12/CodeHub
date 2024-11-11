﻿namespace CodeHub.Core.Models.Resource;

public enum PipelinePlatform
{
    AzureDevOps,
    GitHubActions,
    GitLab,
    Jenkins,
    TravisCi
}

public abstract record PipelineResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required Uri Url { get; set; }
    public required PipelinePlatform Platform { get; set; }
}