namespace CodeHub.Engine.SooS.Models;

internal struct SoosConstants
{
    internal const string ApiKeyHeaderName = "x-soos-apikey";
    private const string ProjectsUrl = "https://api-projects.soos.io/api";

    // Get statistics for a branch scan: 
    // https://api-projects.soos.io/api/clients/<clientid>/projects/<projectid>/branches/<branchid>/scan-types/sca/statistics/issues?skip=0&take=2
    
    internal static string GetProjectsUrl(string clientId)
    {
        return $"https://api-projects.soos.io/api/clients/{clientId}/projects";
    }

    internal static string GetProjectBranchesUrl(string clientId, string projectId)
    {
        return $"https://api-projects.soos.io/api/clients/{clientId}/projects/{projectId}/branches";
    }

    internal static string GetProjectSettingsUrl(string clientId, string projectId)
    {
        return $"https://api-projects.soos.io/api/clients/{clientId}/projects/{projectId}/settings?fallback=true";
    }

    internal static string GetProjectBranchScanNewUrl(string clientId, string projectId, string branchId)
    {
        return
            $"https://api-projects.soos.io/api/clients/{clientId}/projects/{projectId}/branches/{branchId}/issues?scanType=sca&status=new&skip=0&take=10000";
    }
    
    internal static string GetProjectBranchScanPendingUrl(string clientId, string projectId, string branchId)
    {
        return
            $"https://api-projects.soos.io/api/clients/{clientId}/projects/{projectId}/branches/{branchId}/issues?scanType=sca&status=pending&skip=0&take=10000";
    }

    internal static string GetAnalysisUrl(string clientId)
    {
        return $"https://api.soos.io/api/clients/{clientId}/scan-types/sca/scans";
    }
}