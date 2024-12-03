namespace CodeHub.Platform.Azure.Models;

internal sealed record AzureSettings
{
    public required bool IsEnabled { get; init; }
}