namespace CodeHub.Core.Platforms.SonarCloud;

public interface ISonarCloudService
{
    Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken);
}