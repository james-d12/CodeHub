using CodeHub.Core.Platforms.Soos.Extensions;
using CodeHub.Core.Platforms.Soos.Models;
using CodeHub.Core.Platforms.Soos.Services;
using CodeHub.Core.Platforms.Soos.Validation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Tests.Platforms.SooS;

public sealed class SooSExtensionsTests
{
    [Fact]
    public void RegisterSoosServices_WhenCalledInValidEnvironment_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidSooSConfiguration())
            .Build();

        // Act
        serviceCollection.RegisterSoosServices(configuration);

        // Assert
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(ISoosService) &&
                       service.Lifetime == ServiceLifetime.Transient &&
                       service.ImplementationType == typeof(SoosService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IValidateOptions<SoosSettings>) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(SoosSettingsValidation));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IMemoryCache) &&
                       service.ImplementationType == typeof(MemoryCache));
    }

    [Fact]
    public void RegisterSoosServices_WhenCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterSoosServices(configuration));
    }

    private static Dictionary<string, string?> GetValidSooSConfiguration()
    {
        return new Dictionary<string, string?>
        {
            { "SoosSettings:ClientId", "TestClientId" },
            { "SoosSettings:Key", "TestKey" }
        };
    }
}