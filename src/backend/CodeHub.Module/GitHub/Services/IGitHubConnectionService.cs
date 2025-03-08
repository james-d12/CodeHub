using Octokit;

namespace CodeHub.Module.GitHub.Services;

public interface IGitHubConnectionService
{
    GitHubClient Client { get; }
}