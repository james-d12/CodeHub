using CodeHub.Platform.SooS.Models;

namespace CodeHub.Platform.SooS.Services;

internal interface ISoosCacheService
{
    void SetProjects(List<SoosProject> projects);
    List<SoosProject> GetProjects();
}