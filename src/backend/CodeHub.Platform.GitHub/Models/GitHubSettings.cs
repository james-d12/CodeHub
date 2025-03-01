using CodeHub.Shared.Models;

namespace CodeHub.Platform.GitHub.Models;

internal sealed class GitHubSettings : Settings
{
    public string AgentName { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}