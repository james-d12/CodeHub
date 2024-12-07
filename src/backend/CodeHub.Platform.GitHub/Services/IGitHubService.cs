using CodeHub.Shared.Models;
using Repository = CodeHub.Shared.Models.Repository;

namespace CodeHub.Platform.GitHub.Services;

public interface IGitHubService
{
    Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken);
    Task<List<Pipeline>> GetActionsAsync(string owner, string repository, CancellationToken cancellationToken);
}