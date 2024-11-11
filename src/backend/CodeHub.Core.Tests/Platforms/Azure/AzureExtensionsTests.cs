using CodeHub.Core.Platforms.Azure;
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
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IDiscoveryService));
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IAzureService));
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IMemoryCache));
    }
}