using CodeHub.Platform.Azure.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.Azure.Validation;

internal static class AzureSettingsValidator
{
    internal static AzureSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(AzureSettings));
        var isEnabledSection = settingsSection.GetSection(nameof(AzureSettings.IsEnabled));

        if (!settingsSection.Exists() || !isEnabledSection.Exists())
        {
            throw new InvalidOperationException("");
        }

        var isEnabled = settingsSection.GetValue<bool>(nameof(AzureSettings.IsEnabled));

        return new AzureSettings
        {
            IsEnabled = isEnabled
        };
    }
}