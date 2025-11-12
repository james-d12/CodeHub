using CodeHub.Module.GitLab.Extensions;
using CodeHub.Module.GitLab.Models;
using CodeHub.Shared;
using Microsoft.Extensions.Logging;
using NGitLab.Models;

namespace CodeHub.Module.GitLab.Services;

public sealed class GitLabService : IGitLabService
{
    private readonly ILogger<GitLabService> _logger;
    private readonly IGitLabConnectionService _connectionService;

    public GitLabService(ILogger<GitLabService> logger, IGitLabConnectionService connectionService)
    {
        _logger = logger;
        _connectionService = connectionService;
    }

    public List<Project> GetProjects()
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Getting GitLab Projects.");

        return _connectionService.Client.Projects.Get(new ProjectQuery
        {
            PerPage = 100
        }).ToList();
    }

    public List<GitLabPullRequest> GetPullRequests()
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Getting GitLab Pull Requests.");

        return _connectionService.Client.MergeRequests.Get(new MergeRequestQuery
        {
            PerPage = 100
        }).Select(r => r.MapToGitLabPullRequest()).ToList();
    }

    public List<GitLabPipeline> GetPipelines(Project project)
    {
        using var activity = Tracing.StartActivity();
        _logger.LogInformation("Getting GitLab Pipelines.");

        return _connectionService.Client
            .GetPipelines(new ProjectId(project.Id)).All
            .Select(p => p.MapToGitLabPipeline()).ToList();
    }
}