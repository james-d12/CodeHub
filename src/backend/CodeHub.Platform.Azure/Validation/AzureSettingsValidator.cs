using CodeHub.Platform.Azure.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.Azure.Validation;

internal static class AzureSettingsValidator
{
    internal static AzureSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(AzureSettings));

        if (!settingsSection.Exists())
        {
            throw new InvalidOperationException("Azure settings section is missing");
        }

        var isEnabledSection = settingsSection.GetSection(nameof(AzureSettings.IsEnabled));
        var isEnabled = settingsSection.GetValue<bool>(nameof(AzureSettings.IsEnabled));

        if (!isEnabledSection.Exists() || !isEnabled)
        {
            return AzureSettings.CreateDisabled();
        }

        return new AzureSettings
        {
            IsEnabled = isEnabled
        };
    }
}