using Microsoft.VisualStudio.Services.WebApi;

namespace CodeHub.Platform.AzureDevOps.Services;

internal interface IAzureDevOpsConnectionService
{
    Task<T> GetClientAsync<T>(CancellationToken cancellationToken) where T : IVssHttpClient;
}