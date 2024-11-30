using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace CodeHub.Platform.AzureDevOps.Services;

public interface IAzureDevOpsService
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(Guid projectId, CancellationToken cancellationToken);
    Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(Guid projectId, CancellationToken cancellationToken);
    Task<List<AzureDevOpsProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken);
    Task<List<WorkItem>> GetWorkItemsAsync(string projectName, CancellationToken cancellationToken);
    Task<List<AzureDevOpsPullRequest>> GetPullRequestsAsync(Guid projectId, CancellationToken cancellationToken);
}