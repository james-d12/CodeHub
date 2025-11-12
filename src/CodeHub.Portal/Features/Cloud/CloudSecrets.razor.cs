using CodeHub.Domain.Cloud;

namespace CodeHub.Portal.Features.Cloud;

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