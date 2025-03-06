using CodeHub.Core.GitHub.Extensions;
using CodeHub.Core.GitHub.Services;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Core.Tests.GitHub.Extensions;

public sealed class GitHubExtensionsTests
{
    [Fact]
    public void RegisterGitHubServices_WhenEnabledWithValidSettings_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidGitHubConfiguration(true))
            .Build();

        // Act
        serviceCollection.RegisterGitHub(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitHubDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IGitHubService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitHubService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IGitHubConnectionService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(GitHubConnectionService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterGitHubServices_WhenDisabledWithValidSettings_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidGitHubConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterGitHub(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }

    [Fact]
    public void RegisterGitHubServices_WhenDisabledWithValidSettingsButNotWholeSection_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidWithoutOtherGitHubConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterGitHub(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }


    [Fact]
    public void RegisterGitHubServices_WhenEnabledButCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetInvalidGitHubSettings(true))
            .Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterGitHub(configuration));
    }

    private static Dictionary<string, string?> GetValidGitHubConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitHubSettings:AgentName", "TestAgentName" },
            { "GitHubSettings:Token", "TestToken" },
            { "GitHubSettings:IsEnabled", enabled.ToString() }
        };
    }

    private static Dictionary<string, string?> GetValidWithoutOtherGitHubConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitHubSettings:IsEnabled", enabled.ToString() }
        };
    }

    private static Dictionary<string, string?> GetInvalidGitHubSettings(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "GitHubSettings:IsEnabled", enabled.ToString() }
        };
    }
}