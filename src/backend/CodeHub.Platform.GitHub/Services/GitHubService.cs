using CodeHub.Platform.GitHub.Extensions;
using CodeHub.Shared.Models;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubService : IGitHubService
{
    private readonly IGitHubConnectionService _gitHubConnectionService;

    public GitHubService(IGitHubConnectionService gitHubConnectionService)
    {
        _gitHubConnectionService = gitHubConnectionService;
    }

    public async Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken)
    {
        var repositories =
            await _gitHubConnectionService.Client().Repository.GetAllForCurrent().WaitAsync(cancellationToken) ?? [];
        return repositories.Select(r => r.MapToRepository()).ToList();
    }
}