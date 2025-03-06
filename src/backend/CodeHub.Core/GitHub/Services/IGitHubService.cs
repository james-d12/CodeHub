using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.GitHub.Services;

public interface IGitHubService
{
    Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken);
    Task<List<Pipeline>> GetActionsAsync(string owner, string repository, CancellationToken cancellationToken);
}