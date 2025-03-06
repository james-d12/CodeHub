using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.AzureDevOps.Models;

public sealed class AzureDevOpsSettings : Settings
{
    public string PersonalAccessToken { get; init; } = string.Empty;
    public string Organization { get; init; } = string.Empty;
}