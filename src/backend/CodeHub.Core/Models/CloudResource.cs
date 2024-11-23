namespace CodeHub.Core.Models;

public enum CloudPlatform
{
    Azure,
    Aws,
    GoogleCloud
}

public abstract record CloudResource
{
    public required string Id { get; set; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Uri Url { get; init; }
    public required CloudPlatform Platform { get; init; }
}