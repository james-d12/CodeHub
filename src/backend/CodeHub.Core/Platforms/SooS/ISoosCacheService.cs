namespace CodeHub.Core.Platforms.SooS;

internal interface ISoosCacheService
{
    void SetProjects(List<SoosProject> projects);
    List<SoosProject> GetProjects();
}