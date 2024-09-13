using CodeHub.Platform.SonarCloud.Models;

namespace CodeHub.Platform.SonarCloud.Services;

public interface ISonarCloudService
{
    Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken);
}