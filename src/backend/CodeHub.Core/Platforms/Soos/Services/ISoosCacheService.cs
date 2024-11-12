using CodeHub.Core.Platforms.Soos.Models;

namespace CodeHub.Core.Platforms.Soos.Services;

internal interface ISoosCacheService
{
    void SetProjects(List<SoosProject> projects);
    List<SoosProject> GetProjects();
}