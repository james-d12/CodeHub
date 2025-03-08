using CodeHub.Module.Shared;

namespace CodeHub.Module.Tests.Shared.Validation;

internal sealed class ValidationBuilderTestSettings : Settings
{
    public string TestProperty { get; init; } = string.Empty;
}