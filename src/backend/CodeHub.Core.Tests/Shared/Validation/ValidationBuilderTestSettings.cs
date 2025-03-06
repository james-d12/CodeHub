using CodeHub.Core.Shared.Models;

namespace CodeHub.Core.Tests.Shared.Validation;

public sealed class ValidationBuilderTestSettings : Settings
{
    public string TestProperty { get; init; } = string.Empty;
}