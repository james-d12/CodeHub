using CodeHub.Module.GitHub.Models;
using Microsoft.Extensions.Options;
using Octokit;

namespace CodeHub.Module.GitHub.Services;

public sealed class GitHubConnectionService : IGitHubConnectionService
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