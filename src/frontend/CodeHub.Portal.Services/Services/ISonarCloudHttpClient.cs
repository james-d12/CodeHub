using CodeHub.Platform.SonarCloud.Models;

namespace CodeHub.Portal.Services.Services;

public interface ISonarCloudHttpClient
{
    
    Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync();
}