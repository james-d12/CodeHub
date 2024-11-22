using CodeHub.Core.Platforms.Azure.Extensions;
using CodeHub.Core.Platforms.Azure.Services;
using CodeHub.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Core.Tests.Platforms.Azure;

public sealed class AzureExtensionsTests
{
    [Fact]
    public void RegisterAzureServices_WhenCalledInValidEnvironment_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.RegisterAzureServices();

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IDiscoveryService) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(AzureDiscoveryService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IAzureService) &&
                       service.Lifetime == ServiceLifetime.Transient &&
                       service.ImplementationType == typeof(AzureService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }
}