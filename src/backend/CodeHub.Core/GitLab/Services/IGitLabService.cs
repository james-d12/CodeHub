using CodeHub.Core.GitLab.Models;
using NGitLab.Models;

namespace CodeHub.Core.GitLab.Services;

public interface IGitLabService
{
    List<Project> GetProjects();
    List<GitLabPullRequest> GetPullRequests();
    List<GitLabPipeline> GetPipelines(Project project);
}