namespace CodeHub.Platform.GitHub.Models;

internal sealed record GitHubSettings
{
    public required string AgentName { get; init; }
    public required string Token { get; init; }
    public required bool IsEnabled { get; init; }
}