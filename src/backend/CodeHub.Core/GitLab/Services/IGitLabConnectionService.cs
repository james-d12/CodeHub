using NGitLab;

namespace CodeHub.Core.GitLab.Services;

public interface IGitLabConnectionService
{
    GitLabClient Client { get; }
}