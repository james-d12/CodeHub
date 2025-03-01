using CodeHub.Platform.GitHub.Models;
using Microsoft.Extensions.Options;
using Octokit;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubConnectionService : IGitHubConnectionService
{
    public GitHubClient Client { get; }

    public GitHubConnectionService(IOptions<GitHubSettings> options)
    {
        Client = new GitHubClient(new ProductHeaderValue(options.Value.AgentName))
        {
            Credentials = new Credentials(options.Value.Token)
        };
    }
}