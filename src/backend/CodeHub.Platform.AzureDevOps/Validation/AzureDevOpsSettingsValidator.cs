using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.AzureDevOps.Validation;

internal static class AzureDevOpsSettingsValidator
{
    internal static AzureDevOpsSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(AzureDevOpsSettings));

        if (!settingsSection.Exists())
        {
            throw new InvalidOperationException("Azure DevOps settings section is missing.");
        }

        var isEnabledSection = settingsSection.GetSection(nameof(AzureDevOpsSettings.IsEnabled));
        var isEnabled = settingsSection.GetValue<bool>(nameof(AzureDevOpsSettings.IsEnabled));

        if (!isEnabledSection.Exists() || !isEnabled)
        {
            return AzureDevOpsSettings.CreateDisabled();
        }

        var organization = settingsSection.GetValue<string>(nameof(AzureDevOpsSettings.Organization));
        var personalAccessToken = settingsSection.GetValue<string>(nameof(AzureDevOpsSettings.PersonalAccessToken));

        if (string.IsNullOrEmpty(organization))
        {
            throw new InvalidOperationException("Organization configuration is missing");
        }

        if (string.IsNullOrEmpty(personalAccessToken))
        {
            throw new InvalidOperationException("Personal Access Token configuration is missing");
        }

        return new AzureDevOpsSettings
        {
            Organization = organization,
            PersonalAccessToken = personalAccessToken,
            IsEnabled = isEnabled
        };
    }
}