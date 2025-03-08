using CodeHub.Module.AzureDevOps.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureDevOpsHttpClient
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync();
}