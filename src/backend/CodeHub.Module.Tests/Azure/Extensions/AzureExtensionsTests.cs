using CodeHub.Domain.Cloud.Service;
using CodeHub.Domain.Discovery;
using CodeHub.Module.Azure.Extensions;
using CodeHub.Module.Azure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Module.Tests.Azure.Extensions;

public sealed class AzureExtensionsTests
{
    [Fact]
    public void RegisterAzureServices_WhenCalledInValidEnvironment_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetAzureConfiguration(true))
            .Build();

        // Act
        serviceCollection.RegisterAzure(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(ICloudQueryService) &&
                       service.Lifetime == ServiceLifetime.Scoped &&
                       service.ImplementationType == typeof(AzureCloudQueryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterAzureServices_WhenCalledButAzureIsDisabled_DoesNotRegisterServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetAzureConfiguration(false))
            .Build();

        // Act
        var beforeCount = serviceCollection.Count;
        serviceCollection.RegisterAzure(configuration);

        // Assert
        Assert.Equal(beforeCount, serviceCollection.Count);
    }

    private static Dictionary<string, string?> GetAzureConfiguration(bool isEnabled)
    {
        return new Dictionary<string, string?>
        {
            { "AzureSettings:IsEnabled", isEnabled.ToString() }
        };
    }
}