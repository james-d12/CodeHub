using CodeHub.Domain.Cloud;

namespace CodeHub.Portal.Features.Cloud;

public interface ICloudHttpClient
{
    Task<List<CloudResource>> GetCloudResourcesAsync();
    Task<List<CloudSecret>> GetCloudSecretsAsync();
}