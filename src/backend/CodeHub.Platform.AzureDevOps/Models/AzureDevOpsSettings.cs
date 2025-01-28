namespace CodeHub.Platform.AzureDevOps.Models;

internal sealed record AzureDevOpsSettings
{
    public required string PersonalAccessToken { get; init; }
    public required string Organization { get; init; }
    public required bool IsEnabled { get; init; }

    public static AzureDevOpsSettings CreateDisabled()
    {
        return new AzureDevOpsSettings
        {
            IsEnabled = false,
            PersonalAccessToken = string.Empty,
            Organization = string.Empty,
        };
    }
}