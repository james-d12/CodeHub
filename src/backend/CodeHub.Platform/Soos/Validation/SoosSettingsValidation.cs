using CodeHub.Platform.Soos.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.Soos.Validation;

internal sealed class SoosSettingsValidation : IValidateOptions<SoosSettings>
{
    public ValidateOptionsResult Validate(string? name, SoosSettings options)
    {
        if (string.IsNullOrEmpty(options.ClientId))
        {
            return ValidateOptionsResult.Fail("Soos Client Id must not be null or empty.");
        }

        if (string.IsNullOrEmpty(options.Key))
        {
            return ValidateOptionsResult.Fail("Soos Key must not be null or empty.");
        }

        return ValidateOptionsResult.Success;
    }
}