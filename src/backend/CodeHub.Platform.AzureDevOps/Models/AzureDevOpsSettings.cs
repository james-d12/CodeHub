namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsSettings
{
    public required string PersonalAccessToken { get; init; }
    public required string Organization { get; init; }
}