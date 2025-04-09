using CodeHub.Module.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.Tests.Shared.Validation;

public sealed class ValidationBuilderTests
{
    [Fact]
    public void SectionExists_WhenSectionDoesNotExist_ThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => sut.SectionExists("SectionThatDoesNotExist"));
    }

    [Fact]
    public void CheckEnabled_WhenSettingsSectionDoesNotExist_ThrowsArgumentNullException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act + Assert
        Assert.Throws<ArgumentNullException>(() =>
            sut.CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled)));
    }

    [Fact]
    public void CheckEnabled_WhenIsEnabledDoesNotExist_SetsSettingsIsEnabledToFalse()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetTestSettingsWithSection()).Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act
        var result = sut
            .SectionExists("TestSettings")
            .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
            .Build();

        // Assert
        Assert.False(result.IsEnabled);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void CheckEnabled_WhenIsEnabledExists_SetsSettingsValue(bool isEnabled)
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetValidTestSettings(isEnabled)).Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act
        var result = sut
            .SectionExists("TestSettings")
            .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
            .Build();

        // Assert
        Assert.Equal(isEnabled, result.IsEnabled);
    }

    [Fact]
    public void CheckValue_WhenSettingsSectionDoesNotExist_ThrowsArgumentNullException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act + Assert
        Assert.Throws<ArgumentNullException>(() =>
            sut.CheckValue(x => x.TestProperty, nameof(ValidationBuilderTestSettings.TestProperty)));
    }

    [Fact]
    public void CheckValue_WhenIsEnabledIsFalse_DoesNotSetSettingsValue()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetValidTestSettings(false)).Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act
        var result = sut
            .SectionExists("TestSettings")
            .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
            .CheckValue(x => x.TestProperty, nameof(ValidationBuilderTestSettings.TestProperty))
            .Build();

        // Assert
        Assert.False(result.IsEnabled);
        Assert.Equal(string.Empty, result.TestProperty);
    }

    [Fact]
    public void CheckValue_WhenIsEnabledButPropertyIsNull_ThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetIsEnabledOnlyTestSettings(true))
            .Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() =>
            sut
                .SectionExists("TestSettings")
                .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
                .CheckValue(x => x.TestProperty, nameof(ValidationBuilderTestSettings.TestProperty))
                .Build());
    }

    [Fact]
    public void CheckValue_WhenSettingsAreValid_SetsSettingsValue()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetValidTestSettings(true))
            .Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act 
        var settings = sut
            .SectionExists("TestSettings")
            .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
            .CheckValue(x => x.TestProperty, nameof(ValidationBuilderTestSettings.TestProperty))
            .Build();

        // Assert
        Assert.True(settings.IsEnabled);
        Assert.Equal("RandomTestValue", settings.TestProperty);
    }

    [Fact]
    public void CheckValue_ListOfStrings_WhenIsEnabledButListIsMissing_ThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(GetIsEnabledOnlyTestSettings(true))
            .Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() =>
            sut
                .SectionExists("TestSettings")
                .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
                .CheckValue(x => x.TestList, nameof(ValidationBuilderTestSettings.TestList))
                .Build());
    }

    [Fact]
    public void CheckValue_ListOfStrings_WhenSettingsAreValid_SetsListCorrectly()
    {
        // Arrange
        var configData = GetValidTestSettings(true);
        configData.Add("TestSettings:TestList:0", "Value1");
        configData.Add("TestSettings:TestList:1", "Value2");

        var configuration = new ConfigurationBuilder().AddInMemoryCollection(configData).Build();
        var sut = new ValidationBuilder<ValidationBuilderTestSettings>(configuration);

        // Act
        var result = sut
            .SectionExists("TestSettings")
            .CheckEnabled(x => x.IsEnabled, nameof(ValidationBuilderTestSettings.IsEnabled))
            .CheckValue(x => x.TestList, nameof(ValidationBuilderTestSettings.TestList))
            .Build();

        // Assert
        Assert.True(result.IsEnabled);
        Assert.Equal(new[] { "Value1", "Value2" }, result.TestList);
    }

    private static Dictionary<string, string?> GetTestSettingsWithSection()
    {
        return new Dictionary<string, string?>
        {
            { "TestSettings", "" }
        };
    }

    private static Dictionary<string, string?> GetValidTestSettings(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "TestSettings:IsEnabled", enabled.ToString() },
            { "TestSettings:TestProperty", "RandomTestValue" },
            { "TestSettings:TestList", "['Test']" }
        };
    }

    private static Dictionary<string, string?> GetIsEnabledOnlyTestSettings(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "TestSettings:IsEnabled", enabled.ToString() }
        };
    }
}