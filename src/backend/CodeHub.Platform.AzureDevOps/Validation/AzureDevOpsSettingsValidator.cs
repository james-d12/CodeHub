using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.AzureDevOps.Validation;

internal static class AzureDevOpsSettingsValidator
{
    internal static AzureDevOpsSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<AzureDevOpsSettings>(configuration)
            .SectionExists(nameof(AzureDevOpsSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(AzureDevOpsSettings.IsEnabled))
            .CheckValue(x => x.Organization, nameof(AzureDevOpsSettings.Organization))
            .CheckValue(x => x.PersonalAccessToken, nameof(AzureDevOpsSettings.PersonalAccessToken))
            .Build();
    }
}