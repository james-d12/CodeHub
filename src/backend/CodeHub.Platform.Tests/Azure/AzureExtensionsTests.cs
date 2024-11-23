using CodeHub.Core.Services;
using CodeHub.Platform.Azure.Extensions;
using CodeHub.Platform.Azure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Platform.Tests.Azure;

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