using CodeHub.Core.AzureDevOps.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureDevOpsHttpClient
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync();
}