using CodeHub.Domain.Discovery;
using CodeHub.Domain.Git.Service;
using CodeHub.Domain.Ticketing.Service;
using CodeHub.Module.AzureDevOps.Extensions;
using CodeHub.Module.AzureDevOps.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Module.Tests.AzureDevOps.Extensions;

public sealed class AzureDevOpsExtensionsTests
{
    [Fact]
    public void RegisterAzureDevOpsServices_WhenEnabledWithValidSettings_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidAzureDevOpsConfiguration(true))
            .Build();

        // Act
        serviceCollection.RegisterAzureDevOps(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsConnectionService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsConnectionService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IGitQueryService) &&
                       service.Lifetime == ServiceLifetime.Scoped &&
                       service.ImplementationType == typeof(AzureDevOpsGitQueryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsQueryService) &&
                       service.Lifetime == ServiceLifetime.Scoped &&
                       service.ImplementationType == typeof(AzureDevOpsQueryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(ITicketingQueryService) &&
                       service.Lifetime == ServiceLifetime.Scoped &&
                       service.ImplementationType == typeof(AzureDevOpsTicketingQueryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterAzureDevOpsServices_WhenDisabledWithValidSettings_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidAzureDevOpsConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterAzureDevOps(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }

    [Fact]
    public void RegisterAzureDevOpsServices_WhenDisabledWithValidSettingsButNotWholeSection_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidWithoutOtherAzureDevOpsConfiguration(false))
            .Build();

        // Act
        var serviceCountBefore = serviceCollection.Count;
        serviceCollection.RegisterAzureDevOps(configuration);

        // Assert
        Assert.Equal(serviceCountBefore, serviceCollection.Count);
    }


    [Fact]
    public void RegisterAzureDevOpsServices_WhenEnabledButCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetInvalidAzureSettings(true))
            .Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterAzureDevOps(configuration));
    }

    private static Dictionary<string, string?> GetValidAzureDevOpsConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:Organization", "TestOrganization" },
            { "AzureDevOpsSettings:PersonalAccessToken", "TestPersonalAccessToken" },
            { "AzureDevOpsSettings:IsEnabled", enabled.ToString() },
            { "AzureDevOpsSettings:ProjectFilters:0", "Project1" },
            { "AzureDevOpsSettings:ProjectFilters:1", "Project2" },
            { "AzureDevOpsSettings:ProjectFilters:2", "Project3" }
        };
    }

    private static Dictionary<string, string?> GetValidWithoutOtherAzureDevOpsConfiguration(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:IsEnabled", enabled.ToString() }
        };
    }

    private static Dictionary<string, string?> GetInvalidAzureSettings(bool enabled)
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:IsEnabled", enabled.ToString() }
        };
    }
}