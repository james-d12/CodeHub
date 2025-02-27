using Microsoft.Extensions.Logging;
using NGitLab.Models;

namespace CodeHub.Platform.GitLab.Services;

internal sealed class GitLabService : IGitLabService
{
    private readonly ILogger<GitLabService> _logger;
    private readonly IGitLabConnectionService _connectionService;

    public GitLabService(ILogger<GitLabService> logger, IGitLabConnectionService connectionService)
    {
        _logger = logger;
        _connectionService = connectionService;
    }

    public void GetProjects()
    {
        _logger.LogInformation("Getting GitLab Projects.");

        var projects = _connectionService.Client.Projects.Get(new ProjectQuery
        {
            PerPage = 100
        }).ToList();
    }
}