using CodeHub.Core.Platforms.Soos.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CodeHub.Core.Platforms.Soos.Services;

internal sealed class SoosCacheService(IMemoryCache memoryCache) : ISoosCacheService
{
    private const string ProjectKey = "soos-projects";

    public void SetProjects(List<SoosProject> projects)
    {
        SetItem(projects, ProjectKey);
    }

    public List<SoosProject> GetProjects()
    {
        return memoryCache.Get<List<SoosProject>>(ProjectKey) ?? [];
    }

    private void SetItem<T>(T item, string id)
    {
        if (!memoryCache.TryGetValue(id, out _))
        {
            memoryCache.Set(id, item);
        }
    }
}