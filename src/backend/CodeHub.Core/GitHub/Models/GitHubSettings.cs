using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.GitHub.Models;

public sealed class GitHubSettings : Settings
{
    public string AgentName { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}