using CodeHub.Core.Models;

namespace CodeHub.Core.Services;

public interface IRepositoryResourceService
{
    Task<List<RepositoryResource>> GetRepositoryResourcesAsync(CancellationToken cancellationToken);
    Task<RepositoryResource?> GetRepositoryResourceAsync(string id, CancellationToken cancellationToken);
}