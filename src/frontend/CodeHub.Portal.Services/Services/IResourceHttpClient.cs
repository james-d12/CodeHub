using CodeHub.Shared.Models;

namespace CodeHub.Portal.Services.Services;

public interface IResourceHttpClient
{
    Task<List<Pipeline>> GetPipelines();
    Task<List<Repository>> GetRepositories();
}