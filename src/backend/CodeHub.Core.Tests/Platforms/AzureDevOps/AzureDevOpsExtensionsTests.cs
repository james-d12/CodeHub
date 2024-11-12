using CodeHub.Core.Platforms.AzureDevOps;
using CodeHub.Core.Platforms.AzureDevOps.Extensions;
using CodeHub.Core.Platforms.AzureDevOps.Services;
using CodeHub.Core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IDiscoveryService));
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IAzureDevOpsService));
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(IMemoryCache));
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