namespace CodeHub.Domain.Cloud;

public sealed record CloudSecret
{
    public required string Name { get; init; }
    public required string Location { get; init; }
    public required Uri Url { get; init; }
}