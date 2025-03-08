using CodeHub.Module.GitHub.Extensions;
using CodeHub.Module.GitHub.Models;

namespace CodeHub.Module.GitHub.Services;

public sealed class GitHubService : IGitHubService
{
    private readonly IGitHubConnectionService _gitHubConnectionService;

    public GitHubService(IGitHubConnectionService gitHubConnectionService)
    {
        _gitHubConnectionService = gitHubConnectionService;
    }

    public async Task<List<GitHubRepository>> GetRepositoriesAsync(CancellationToken cancellationToken)
    {
        var repositories =
            await _gitHubConnectionService.Client.Repository.GetAllForCurrent().WaitAsync(cancellationToken) ?? [];
        return repositories.Select(r => r.MapToGitHubRepository()).ToList();
    }

    public async Task<List<GitHubPipeline>> GetActionsAsync(string owner, string repository,
        CancellationToken cancellationToken)
    {
        var pipelines = await _gitHubConnectionService.Client.Actions.Workflows.List(owner, repository);
        var jobs = await _gitHubConnectionService.Client.Actions.Workflows.Runs.List(owner, repository);

        var workflows = pipelines.Workflows.GroupJoin(jobs.WorkflowRuns,
            workflow => workflow.Id,
            workflowRuns => workflowRuns.WorkflowId,
            (workflow, workflowRuns) => new { workflow, workflowRuns });

        return pipelines.Workflows.Select(w => w.MapToGitHubPipeline()).ToList();
    }

    public async Task<List<GitHubPullRequest>> GetPullRequestsAsync(GitHubRepository repository)
    {
        var parsedId = long.TryParse(repository.Id.Value, out var repositoryId);

        if (!parsedId)
        {
            return [];
        }

        var pullRequests = await _gitHubConnectionService.Client.PullRequest.GetAllForRepository(repositoryId);
        return pullRequests.Select(p => p.MapToGitHubPullRequest(repository)).ToList();
    }
}