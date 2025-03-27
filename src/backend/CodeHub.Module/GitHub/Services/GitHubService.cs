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

    public async Task<List<GitHubPipeline>> GetPipelinesAsync(
        GitHubRepository repository,
        CancellationToken cancellationToken)
    {
        var pipelines =
            await _gitHubConnectionService.Client.Actions.Workflows.List(repository.Owner.Name, repository.Name);
        return pipelines.Workflows.Select(w => w.MapToGitHubPipeline(repository)).ToList();
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