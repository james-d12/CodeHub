using NGitLab;

namespace CodeHub.Platform.GitLab.Services;

internal interface IGitLabConnectionService
{
    GitLabClient Client { get; }
}