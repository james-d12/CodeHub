using CodeHub.Core.Platforms.AzureDevOps;

namespace CodeHub.Portal.Services.Services;

public interface IAzureDevOpsHttpClient
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync();
}