using CodeHub.Platform.Soos.Models;

namespace CodeHub.Platform.Soos.Services;

public interface ISoosService
{
    Task<SoosProject?> GetProjectAsync(string projectId, CancellationToken cancellationToken);
    Task<List<SoosProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<SoosProjectBranch>> GetProjectBranchesAsync(string projectId, CancellationToken cancellationToken);
    Task<SoosProjectSetting?> GetProjectSettingsAsync(string projectId, CancellationToken cancellationToken);
    //Task<List<SoosVulnerability>> GetProjectBranchVulnerabilitiesAsync(string projectId, string branchId, CancellationToken cancellationToken);

    Task<List<SoosProjectBranchVulnerabilityResults>> GetProjectBranchVulnerabilities(string projectId, string branchId,
        CancellationToken cancellationToken);

    Task<List<SoosProjectBranchVulnerabilityResults>> GetProjectVulnerabilitiesAsync(string projectId,
        CancellationToken cancellationToken);
}