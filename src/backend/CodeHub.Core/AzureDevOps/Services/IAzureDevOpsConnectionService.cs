using Microsoft.VisualStudio.Services.WebApi;

namespace CodeHub.Core.AzureDevOps.Services;

public interface IAzureDevOpsConnectionService
{
    Task<T> GetClientAsync<T>(CancellationToken cancellationToken) where T : IVssHttpClient;
}