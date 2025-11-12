using CodeHub.Domain.Cloud;

namespace CodeHub.Portal.Features.Cloud;

public partial class CloudResources
{
    private readonly ICloudHttpClient _cloudHttpClient;
    private List<CloudResource>? _cloudResources;

    public CloudResources(ICloudHttpClient cloudHttpClient)
    {
        _cloudHttpClient = cloudHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _cloudResources = await _cloudHttpClient.GetCloudResourcesAsync();
    }
}