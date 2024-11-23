using CodeHub.Platform.SonarCloud.Models;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class SecurityAnalysisResources
{
    private readonly ILogger<SecurityAnalysisResources> _logger;
    private readonly ISonarCloudHttpClient _sonarCloudHttpClient;
    private SonarCloudResponse<SonarCloudComponent>? _sonarCloudResources;

    private const string IconStr =
        "<image width=\"32\" height=\"32\" xlink:href=\"images/Azure.svg\" />";


    public SecurityAnalysisResources(ILogger<SecurityAnalysisResources> logger,
        ISonarCloudHttpClient sonarCloudHttpClient)
    {
        _logger = logger;
        _sonarCloudHttpClient = sonarCloudHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Security Analysis Resources");
        _sonarCloudResources = await _sonarCloudHttpClient.GetComponentsAsync();
    }
}