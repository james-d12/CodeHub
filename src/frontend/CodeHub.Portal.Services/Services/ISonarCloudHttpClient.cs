using CodeHub.Core.Platforms.SonarCloud;

namespace CodeHub.Portal.Services.Services;

public interface ISonarCloudHttpClient
{
    
    Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync();
}