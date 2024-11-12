using CodeHub.Core.Platforms.Soos.Extensions;
using CodeHub.Core.Platforms.Soos.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(ISoosService));
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IMemoryCache));
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