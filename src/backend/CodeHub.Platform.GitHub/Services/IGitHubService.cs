using CodeHub.Shared.Models;

namespace CodeHub.Platform.GitHub.Services;

public interface IGitHubService
{
    Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken);
}