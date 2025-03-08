using NGitLab;

namespace CodeHub.Module.GitLab.Services;

public interface IGitLabConnectionService
{
    GitLabClient Client { get; }
}