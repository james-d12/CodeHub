using CodeHub.Platform.GitLab.Extensions;
using CodeHub.Platform.GitLab.Services;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Platform.GitLab.Tests.Extensions;

public sealed class GitLabExtensionsTests
{
    [Fact]
    public void RegisterGitLabServices_WhenEnabledWithValidSettings_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidGitLabConfiguration(true))
            .Build();

        // Act
        serviceCollection.RegisterGitLab(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitLabDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IGitLabService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitLabService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IGitLabConnectionService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitLabConnectionService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterGitLabServices_WhenDisabledWithValidSettings_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidGitLabConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterGitLab(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }

    [Fact]
    public void RegisterGitLabServices_WhenDisabledWithValidSettingsButNotWholeSection_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidWithoutOtherGitLabConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterGitLab(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }


    [Fact]
    public void RegisterGitLabServices_WhenEnabledButCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetInvalidGitLabSettings(true))
            .Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterGitLab(configuration));
    }

    private static Dictionary<string, string?> GetValidGitLabConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitLabSettings:HostUrl", "TestHostUrl" },
            { "GitLabSettings:Token", "TestToken" },
            { "GitLabSettings:IsEnabled", enabled.ToString() }
        };
    }

    private static Dictionary<string, string?> GetValidWithoutOtherGitLabConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitLabSettings:IsEnabled", enabled.ToString() }
        };
    }

    private static Dictionary<string, string?> GetInvalidGitLabSettings(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitLabSettings:IsEnabled", enabled.ToString() }
        };
    }
}