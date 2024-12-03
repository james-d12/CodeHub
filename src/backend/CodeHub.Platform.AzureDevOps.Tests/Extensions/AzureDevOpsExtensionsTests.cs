using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Services;
using CodeHub.Platform.AzureDevOps.Validation;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.AzureDevOps.Tests.Extensions;

public sealed class AzureDevOpsExtensionsTests
{
    [Fact]
    public void RegisterAzureDevOpsServices_WhenCalledInValidEnvironment_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidAzureDevOpsConfiguration())
            .Build();

        // Act
        serviceCollection.RegisterAzureDevOps(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsQueryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsQueryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsConnectionService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsConnectionService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IValidateOptions<AzureDevOpsSettings>) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsSettingsValidation));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterAzureDevOpsServices_WhenEnabledButCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetAzureEnabledSettings())
            .Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterAzureDevOps(configuration));
    }

    private static Dictionary<string, string?> GetValidAzureDevOpsConfiguration()
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:Organization", "TestOrganization" },
            { "AzureDevOpsSettings:PersonalAccessToken", "TestPersonalAccessToken" },
            { "AzureDevOpsSettings:IsEnabled", "true" }
        };
    }

    private static Dictionary<string, string?> GetAzureEnabledSettings()
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:IsEnabled", "true" }
        };
    }
}