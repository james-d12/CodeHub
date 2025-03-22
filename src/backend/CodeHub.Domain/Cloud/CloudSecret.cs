namespace CodeHub.Domain.Cloud;

public enum CloudSecretPlatform
{
    Azure,
    Aws,
    GoogleCloud,
}

public sealed record CloudSecret
{
    public required string Name { get; init; }
    public required string Location { get; init; }
    public required Uri Url { get; init; }
    public required CloudSecretPlatform Platform { get; init; }
}