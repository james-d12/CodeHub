using CodeHub.Core.Platforms.AzureDevOps;
using CodeHub.Core.Platforms.AzureDevOps.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureDevOpsHttpClient
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync();
}