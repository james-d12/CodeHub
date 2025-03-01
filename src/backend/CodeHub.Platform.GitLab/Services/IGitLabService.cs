using CodeHub.Platform.GitLab.Models;
using NGitLab.Models;

namespace CodeHub.Platform.GitLab.Services;

public interface IGitLabService
{
    List<Project> GetProjects();
    List<GitLabPullRequest> GetPullRequests();
    List<GitLabPipeline> GetPipelines(Project project);
}