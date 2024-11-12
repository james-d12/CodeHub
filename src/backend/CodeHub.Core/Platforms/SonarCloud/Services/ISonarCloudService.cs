using CodeHub.Core.Platforms.SonarCloud.Models;

namespace CodeHub.Core.Platforms.SonarCloud.Services;

public interface ISonarCloudService
{
    Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken);
}