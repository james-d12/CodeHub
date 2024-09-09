using CodeHub.Engine.SonarCloud.Models;

namespace CodeHub.Engine.SonarCloud.Services;

public interface ISonarCloudService
{
    Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken);
}