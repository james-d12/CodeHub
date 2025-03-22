using CodeHub.Domain.Cloud;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class CloudSecrets
{
    private readonly ICloudHttpClient _cloudHttpClient;
    private List<CloudSecret>? _cloudSecrets;

    public CloudSecrets(ICloudHttpClient cloudHttpClient)
    {
        _cloudHttpClient = cloudHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _cloudSecrets = await _cloudHttpClient.GetCloudSecretsAsync();
    }
}