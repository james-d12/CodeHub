using CodeHub.Domain.Cloud.Request;

namespace CodeHub.Domain.Cloud.Service;

public interface ICloudQueryService
{
    List<CloudResource> QueryCloudResources(CloudResourceQueryRequest request);
}