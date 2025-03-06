namespace CodeHub.Core.Shared.Models;

public enum OwnerPlatform
{
    AzureDevOps,
    GitHub,
    GitLab
}

public readonly record struct OwnerId(string Value);

public record Owner
{
    public required OwnerId Id { get; set; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required Uri Url { get; init; }
    public required OwnerPlatform Platform { get; init; }

    public static Owner CreateEmptyOwner()
    {
        return new Owner
        {
            Id = new OwnerId(string.Empty),
            Name = string.Empty,
            Description = string.Empty,
            Url = new Uri("https://gitlab.com"),
            Platform = OwnerPlatform.GitLab
        };
    }
}