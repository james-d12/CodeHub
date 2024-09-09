namespace CodeHub.Engine.SooS.Models;

internal struct SoosConstants
{
    internal const string ApiKeyHeaderName = "x-soos-apikey";
    private const string ProjectsUrl = "https://api-projects.soos.io/api";
    
    // Get statistics for a branch scan: 
    // https://api-projects.soos.io/api/clients/<clientid>/projects/<projectid>/branches/<branchid>/scan-types/sca/statistics/issues?skip=0&take=2
    
    // Get settings for a project:
    // https://api-projects.soos.io/api/clients/<clientid>/projects/<projectid>/settings?fallback=true
    
    // Get branches for a project
    // https://api-projects.soos.io/api/clients/<clientid>/projects/<projectid>/branches
    
    
    internal static string GetProjectsUrl(string clientId)
    {
        return $"https://api-projects.soos.io/api/clients/{clientId}/projects";
    }

    internal static string GetAnalysisUrl(string clientId)
    {
        return $"https://api.soos.io/api/clients/{clientId}/scan-types/sca/scans";
    }
}