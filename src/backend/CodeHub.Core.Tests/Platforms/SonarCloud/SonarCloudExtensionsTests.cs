using CodeHub.Core.Platforms.SonarCloud;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Core.Tests.Platforms.SonarCloud;

public sealed class SonarCloudExtensionsTests
{
    [Fact]
    public void RegisterSonarCloudServices_WhenCalledInValidEnvironment_RegistersCorrectServices()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(GetValidSonarCloudConfiguration())
            .Build();

        // Act
        serviceCollection.RegisterSonarCloudServices(configuration);

        // Assert
        Assert.Contains(serviceCollection, service => service.ServiceType == typeof(ISonarCloudService));
    }

    private static Dictionary<string, string?> GetValidSonarCloudConfiguration()
    {
        return new Dictionary<string, string?>
        {
            { "SonarCloudSettings:Organization", "TestOrganization" },
            { "SonarCloudSettings:Token", "TestToken" }
        };
    }
}