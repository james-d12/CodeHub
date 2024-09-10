using CodeHub.Engine.SooS.Models;

namespace CodeHub.Engine.SooS.Services;

public interface ISoosService
{
    Task<List<SoosProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<SoosProjectBranch>> GetProjectBranchesAsync(string projectId, CancellationToken cancellationToken);
    Task<SoosProjectSetting?> GetProjectSettingsAsync(string projectId, CancellationToken cancellationToken);
    //Task<List<SoosVulnerability>> GetProjectBranchVulnerabilitiesAsync(string projectId, string branchId, CancellationToken cancellationToken);
}