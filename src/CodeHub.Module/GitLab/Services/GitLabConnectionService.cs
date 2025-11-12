using CodeHub.Module.GitLab.Models;
using Microsoft.Extensions.Options;
using NGitLab;

namespace CodeHub.Module.GitLab.Services;

public sealed class GitLabConnectionService(IOptions<GitLabSettings> options) : IGitLabConnectionService
{
    public GitLabClient Client { get; } = new(options.Value.HostUrl, options.Value.Token);
}