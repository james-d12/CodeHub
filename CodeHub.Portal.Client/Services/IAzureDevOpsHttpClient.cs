using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Portal.Client.Services;

public interface IAzureDevOpsHttpClient
{
    Task<List<GitRepository>> GetRepositoriesAsync();
}