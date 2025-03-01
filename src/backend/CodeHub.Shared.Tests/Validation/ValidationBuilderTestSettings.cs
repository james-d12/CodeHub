using CodeHub.Shared.Models;

namespace CodeHub.Shared.Tests.Validation;

internal sealed class ValidationBuilderTestSettings : Settings
{
    public string TestProperty { get; init; } = string.Empty;
}