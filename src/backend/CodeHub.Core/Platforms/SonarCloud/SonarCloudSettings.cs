namespace CodeHub.Core.Platforms.SonarCloud;

public sealed record SonarCloudSettings
{
    public required string Token { get; init; }
    public required string Organization { get; init; }
}