using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Module.Shared.Validation;
using CodeHub.Shared;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.AzureDevOps.Validation;

public static class AzureDevOpsSettingsValidator
{
    public static AzureDevOpsSettings GetValidSettings(IConfiguration configuration)
    {
        using var activity = Tracing.StartActivity();
        return new ValidationBuilder<AzureDevOpsSettings>(configuration)
            .SectionExists(nameof(AzureDevOpsSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(AzureDevOpsSettings.IsEnabled))
            .CheckValue(x => x.Organization, nameof(AzureDevOpsSettings.Organization))
            .CheckValue(x => x.PersonalAccessToken, nameof(AzureDevOpsSettings.PersonalAccessToken))
            .Build();
    }
}