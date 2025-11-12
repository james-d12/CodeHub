using CodeHub.Module.Shared;

namespace CodeHub.Module.AzureDevOps.Models;

public sealed class AzureDevOpsSettings : Settings
{
    public string PersonalAccessToken { get; init; } = string.Empty;
    public string Organization { get; init; } = string.Empty;
    public List<string> ProjectFilters { get; init; } = [];
}