using CodeHub.Module.Shared;

namespace CodeHub.Module.GitHub.Models;

public sealed class GitHubSettings : Settings
{
    public string AgentName { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}