using CodeHub.Module.AzureDevOps.Extensions;
using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Shared;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace CodeHub.Module.AzureDevOps.Services;

public sealed class AzureDevOpsService : IAzureDevOpsService
{
    private readonly IAzureDevOpsConnectionService _azureDevOpsConnectionService;

    public AzureDevOpsService(IAzureDevOpsConnectionService azureDevOpsConnectionService)
    {
        _azureDevOpsConnectionService = azureDevOpsConnectionService;
    }

    public async Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(Guid projectId,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var gitClient = await _azureDevOpsConnectionService.GetClientAsync<GitHttpClient>(cancellationToken);
        var repositories =
            await gitClient.GetRepositoriesAsync(projectId, cancellationToken: cancellationToken) ?? [];
        return repositories.Select(r => r.MapToAzureDevOpsRepository()).ToList();
    }

    public async Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(
        Guid projectId,
        Uri projectUri,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var buildClient = await _azureDevOpsConnectionService.GetClientAsync<BuildHttpClient>(cancellationToken);
        var pipelines = await buildClient.GetDefinitionsAsync(projectId, cancellationToken: cancellationToken);
        return pipelines.Select(p => p.MapToAzureDevOpsPipeline(projectUri)).ToList();
    }

    public async Task<List<AzureDevOpsProject>> GetProjectsAsync(
        string organisation,
        List<string> projectsFilter,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var projectClient =
            await _azureDevOpsConnectionService.GetClientAsync<ProjectHttpClient>(cancellationToken);
        var projects = await projectClient.GetProjects();

        if (projectsFilter.Count <= 0)
        {
            return projects
                .Select(pr => pr.MapToAzureDevOpsProject(organisation))
                .ToList();
        }

        return projects
            .Where(p => projectsFilter.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
            .Select(pr => pr.MapToAzureDevOpsProject(organisation))
            .ToList();
    }

    public async Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var teamClient = await _azureDevOpsConnectionService.GetClientAsync<TeamHttpClient>(cancellationToken);
        var teams = await teamClient.GetAllTeamsAsync(cancellationToken: cancellationToken);
        return teams.Select(t => t.MapToAzureDevOpsTeam()).ToList();
    }

    public async Task<List<AzureDevOpsWorkItem>> GetWorkItemsAsync(
        string projectName,
        Uri projectUri,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var workItemTrackingClient =
            await _azureDevOpsConnectionService.GetClientAsync<WorkItemTrackingHttpClient>(cancellationToken);
        var wiql = new Wiql
        {
            Query = $@"
                    SELECT 
                        [System.Id],
                        [System.Title],
                        [System.State],
                        [System.WorkItemType]
                    FROM WorkItems
                    WHERE 
                        [System.TeamProject] = '{projectName}' AND
                        [System.State] NOT IN ('Done', 'Completed', 'Closed', 'Resolved')"
        };

        var queryResult = await workItemTrackingClient.QueryByWiqlAsync(wiql, cancellationToken: cancellationToken);

        if (queryResult is null || !queryResult.WorkItems.Any())
        {
            return [];
        }

        var workItemIds = queryResult.WorkItems.Select(item => item.Id).ToList();
        var workItems = new List<WorkItem>();
        const int batchSize = 200;

        for (var i = 0; i < workItemIds.Count; i += batchSize)
        {
            var batchIds = workItemIds.Skip(i).Take(batchSize).ToArray();
            var batchWorkItems =
                await workItemTrackingClient.GetWorkItemsAsync(batchIds,
                    fields:
                    [
                        "System.Title", "System.WorkItemType", "System.State"
                    ], cancellationToken: cancellationToken);
            workItems.AddRange(batchWorkItems);
        }

        return workItems.Select(w => w.MapToAzureDevOpsWorkItem(projectUri)).ToList();
    }

    public async Task<List<AzureDevOpsPullRequest>> GetPullRequestsAsync(
        Guid projectId,
        Uri projectUri,
        CancellationToken cancellationToken)
    {
        using var activity = Tracing.StartActivity();
        var gitHttpClient = await _azureDevOpsConnectionService.GetClientAsync<GitHttpClient>(cancellationToken);
        var criteria = new GitPullRequestSearchCriteria() { Status = PullRequestStatus.Active };
        var pullRequests = await gitHttpClient.GetPullRequestsByProjectAsync(projectId, criteria,
            cancellationToken: cancellationToken);
        return pullRequests.Select(p => p.MapToAzureDevOpsPullRequest(projectUri)).ToList();
    }
}