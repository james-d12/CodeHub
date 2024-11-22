using CodeHub.Core.Platforms.SonarCloud.Extensions;
using CodeHub.Core.Platforms.SonarCloud.Models;
using CodeHub.Core.Platforms.SonarCloud.Services;
using CodeHub.Core.Platforms.SonarCloud.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(ISonarCloudService) &&
                       service.Lifetime == ServiceLifetime.Transient &&
                       service.ImplementationType == typeof(SonarCloudService));
        Assert.Contains(serviceCollection,
            service => service.ServiceType == typeof(IValidateOptions<SonarCloudSettings>) &&
                       service.Lifetime == ServiceLifetime.Singleton &&
                       service.ImplementationType == typeof(SonarCloudSettingsValidation));
    }

    [Fact]
    public void RegisterSonarCloudServices_WhenCalledWithMissingSettings_ThrowsException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => serviceCollection.RegisterSonarCloudServices(configuration));
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