namespace CodeHub.Platform.SonarCloud.Models;

internal sealed record SonarCloudSettings
{
    public required string Token { get; init; }
    public required string Organization { get; init; }
}