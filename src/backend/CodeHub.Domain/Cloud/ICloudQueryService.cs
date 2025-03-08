namespace CodeHub.Domain.Cloud;

public interface ICloudQueryService
{
    List<CloudResource> GetCloudResourcesAsync(CloudResourceQueryRequest request);
}