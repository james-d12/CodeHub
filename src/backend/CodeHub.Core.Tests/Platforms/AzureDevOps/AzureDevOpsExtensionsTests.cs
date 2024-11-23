using CodeHub.Core.Platforms.AzureDevOps.Extensions;
using CodeHub.Core.Platforms.AzureDevOps.Models;
using CodeHub.Core.Platforms.AzureDevOps.Services;
using CodeHub.Core.Platforms.AzureDevOps.Validation;
using CodeHub.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Tests.Platforms.AzureDevOps;

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
        serviceCollection.RegisterAzureDevOpsServices(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDevOpsDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureDevOpsService) &&
                       service.Lifetime == ServiceLifetime.Transient &&
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
    public void RegisterAzureDevOpsServices_WhenCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterAzureDevOpsServices(configuration));
    }

    private static Dictionary<string, string?> GetValidAzureDevOpsConfiguration()
    {
        return new Dictionary<string, string?>
        {
            { "AzureDevOpsSettings:Organization", "TestOrganization" },
            { "AzureDevOpsSettings:PersonalAccessToken", "TestPersonalAccessToken" }
        };
    }
}