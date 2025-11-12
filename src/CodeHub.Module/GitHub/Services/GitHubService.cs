using CodeHub.Module.GitHub.Extensions;
using CodeHub.Module.GitHub.Models;
using CodeHub.Shared;

namespace CodeHub.Module.GitHub.Services;

public sealed class GitHubService : IGitHubService
{
    private readonly IGitHubConnectionService _gitHubConnectionService;

    public GitHubService(IGitHubConnectionService gitHubConnectionService)
    {
        _gitHubConnectionService = gitHubConnectionService;
    }

    public async Task<List<GitHubRepository>> GetRepositoriesAsync()
    {
        using var activity = Tracing.StartActivity();
        var repositories =
            await _gitHubConnectionService.Client.Repository.GetAllForCurrent() ?? [];
        return repositories.Select(r => r.MapToGitHubRepository()).ToList();
    }

    public async Task<List<GitHubPipeline>> GetPipelinesAsync(GitHubRepository repository)
    {
        using var activity = Tracing.StartActivity();
        var pipelines =
            await _gitHubConnectionService.Client.Actions.Workflows.List(repository.Owner.Name, repository.Name);
        return pipelines.Workflows.Select(w => w.MapToGitHubPipeline(repository)).ToList();
    }

    public async Task<List<GitHubPullRequest>> GetPullRequestsAsync(GitHubRepository repository)
    {
        using var activity = Tracing.StartActivity();
        var isParsed = long.TryParse(repository.Id.Value, out var repositoryId);

        if (!isParsed)
        {
            return [];
        }

        var pullRequests = await _gitHubConnectionService.Client.PullRequest.GetAllForRepository(repositoryId);
        return pullRequests.Select(p => p.MapToGitHubPullRequest(repository)).ToList();
    }
}