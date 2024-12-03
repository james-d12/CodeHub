using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.AzureDevOps.Validation;

internal static class AzureDevOpsSettingsValidator
{
    internal static AzureDevOpsSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(AzureDevOpsSettings));
        var isEnabledSection = settingsSection.GetSection(nameof(AzureDevOpsSettings.IsEnabled));

        if (!settingsSection.Exists() || !isEnabledSection.Exists())
        {
            throw new InvalidOperationException("");
        }

        var organization = settingsSection.GetValue<string>(nameof(AzureDevOpsSettings.Organization));
        var personalAccessToken = settingsSection.GetValue<string>(nameof(AzureDevOpsSettings.PersonalAccessToken));
        var isEnabled = settingsSection.GetValue<bool>(nameof(AzureDevOpsSettings.IsEnabled));

        if (string.IsNullOrEmpty(organization) || string.IsNullOrEmpty(personalAccessToken))
        {
            throw new InvalidOperationException("");
        }

        return new AzureDevOpsSettings
        {
            Organization = organization,
            PersonalAccessToken = personalAccessToken,
            IsEnabled = isEnabled
        };
    }
}