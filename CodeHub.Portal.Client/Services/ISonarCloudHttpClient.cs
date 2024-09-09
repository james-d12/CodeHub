using CodeHub.Engine.SonarCloud.Models;

namespace CodeHub.Portal.Client.Services;

public interface ISonarCloudHttpClient
{
    
    Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync();
}