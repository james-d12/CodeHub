using CodeHub.Domain.Cloud;

namespace CodeHub.Portal.Services.Services;

public interface ICloudHttpClient
{
    Task<List<CloudResource>> GetCloudResourcesAsync();
}