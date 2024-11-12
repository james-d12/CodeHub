namespace CodeHub.Core.Platforms.AzureDevOps.Models;

internal sealed record AzureDevOpsSettings
{
    public required string PersonalAccessToken { get; init; }
    public required string Organization { get; init; }
}