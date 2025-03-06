using CodeHub.Core.Azure.Models;
using CodeHub.Core.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Core.Azure.Validation;

internal static class AzureSettingsValidator
{
    internal static AzureSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<AzureSettings>(configuration)
            .SectionExists(nameof(AzureSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(AzureSettings.IsEnabled))
            .Build();
    }
}