namespace CodeHub.Domain.Cloud;

public enum CloudPlatform
{
    Azure,
    Aws,
    GoogleCloud
}

public readonly record struct CloudResourceId(string Value);

public record CloudResource
{
    public required CloudResourceId Id { get; set; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Uri Url { get; init; }
    public required CloudPlatform Platform { get; init; }
}