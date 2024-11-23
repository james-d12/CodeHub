using CodeHub.Platform.SonarCloud.Models;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class StaticAnalysisResources
{
    private readonly ILogger<StaticAnalysisResources> _logger;
    private readonly ISonarCloudHttpClient _sonarCloudHttpClient;

    private SonarCloudResponse<SonarCloudComponent>? _sonarCloudResources;

    public StaticAnalysisResources(ILogger<StaticAnalysisResources> logger, ISonarCloudHttpClient sonarCloudHttpClient)
    {
        _logger = logger;
        _sonarCloudHttpClient = sonarCloudHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Static Analysis Resources");
        _sonarCloudResources = await _sonarCloudHttpClient.GetComponentsAsync();
    }
}