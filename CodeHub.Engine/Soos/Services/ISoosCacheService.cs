using CodeHub.Engine.SooS.Models;

namespace CodeHub.Engine.SooS.Services;

internal interface ISoosCacheService
{
    void SetProjects(List<SoosProject> projects);
    List<SoosProject> GetProjects();
}