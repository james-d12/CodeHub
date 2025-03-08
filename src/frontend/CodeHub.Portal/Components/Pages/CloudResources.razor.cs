using CodeHub.Domain.Cloud;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

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