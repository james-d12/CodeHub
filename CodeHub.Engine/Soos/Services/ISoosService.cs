using CodeHub.Engine.SooS.Models;

namespace CodeHub.Engine.SooS.Services;

public interface ISoosService
{
    Task<List<SoosProject>> GetProjectsAsync(CancellationToken cancellationToken);
}