using CodeHub.Shared.Models;

namespace CodeHub.Platform.AzureDevOps.Models;

internal sealed class AzureDevOpsSettings : Settings
{
    public string PersonalAccessToken { get; init; } = string.Empty;
    public string Organization { get; init; } = string.Empty;
}