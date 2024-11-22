using CodeHub.Core.Platforms.SonarCloud.Models;

namespace CodeHub.Portal.Services.Services;

public interface ISonarCloudHttpClient
{

    Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync();
}