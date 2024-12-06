namespace CodeHub.Shared.Models;

public enum ProjectPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public record Project
{
    public required string Id { get; set; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required Uri Url { get; init; }
    public required ProjectPlatform Platform { get; init; }
}