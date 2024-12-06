using CodeHub.Platform.GitHub.Models;
using Microsoft.Extensions.Options;
using Octokit;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubConnectionService : IGitHubConnectionService
{
    private readonly GitHubClient _client;

    public GitHubConnectionService(IOptions<GitHubSettings> options)
    {
        _client = new GitHubClient(new ProductHeaderValue(options.Value.AgentName))
        {
            Credentials = new Credentials(options.Value.Token)
        };
    }

    public GitHubClient Client()
    {
        return _client;
    }
}