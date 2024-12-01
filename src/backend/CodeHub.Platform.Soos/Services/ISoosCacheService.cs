using CodeHub.Platform.Soos.Models;

namespace CodeHub.Platform.Soos.Services;

internal interface ISoosCacheService
{
    void SetProjects(List<SoosProject> projects);
    List<SoosProject> GetProjects();
}