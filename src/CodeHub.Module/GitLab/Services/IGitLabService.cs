using CodeHub.Module.GitLab.Models;
using NGitLab.Models;

namespace CodeHub.Module.GitLab.Services;

public interface IGitLabService
{
    List<Project> GetProjects();
    List<GitLabPullRequest> GetPullRequests();
    List<GitLabPipeline> GetPipelines(Project project);
}