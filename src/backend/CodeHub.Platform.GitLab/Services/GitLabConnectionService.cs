using CodeHub.Platform.GitLab.Models;
using Microsoft.Extensions.Options;
using NGitLab;

namespace CodeHub.Platform.GitLab.Services;

internal sealed class GitLabConnectionService : IGitLabConnectionService
{
    public GitLabClient Client { get; }

    public GitLabConnectionService(IOptions<GitLabSettings> options)
    {
        Client = new GitLabClient(options.Value.HostUrl, options.Value.Token);
    }
}