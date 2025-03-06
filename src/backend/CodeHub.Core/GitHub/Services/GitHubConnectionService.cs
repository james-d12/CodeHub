using CodeHub.Core.GitHub.Models;
using Microsoft.Extensions.Options;
using Octokit;

namespace CodeHub.Core.GitHub.Services;

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