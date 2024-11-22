using CodeHub.Core.Platforms.SonarCloud.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Platforms.SonarCloud.Validation;

internal sealed class SonarCloudSettingsValidation : IValidateOptions<SonarCloudSettings>
{
    public ValidateOptionsResult Validate(string? name, SonarCloudSettings options)
    {
        if (string.IsNullOrEmpty(options.Organization))
        {
            return ValidateOptionsResult.Fail("Sonarcloud organization must not be null or empty.");
        }

        if (string.IsNullOrEmpty(options.Token))
        {
            return ValidateOptionsResult.Fail("Sonarcloud token must not be null or empty.");
        }

        return ValidateOptionsResult.Success;
    }
}