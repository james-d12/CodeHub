using CodeHub.Portal.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Portal.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAzureDevOpsHttpClient, AzureDevOpsHttpClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5104/azure-devops/");
        });

        services.AddHttpClient<IAzureHttpClient, AzureHttpClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5104/azure/");
        });

        services.AddHttpClient<ISonarCloudHttpClient, SonarCloudHttpClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5104/sonarcloud/");
        });

        services.TryAddScoped<IAzureDevOpsHttpClient, AzureDevOpsHttpClient>();
        services.TryAddScoped<IAzureHttpClient, AzureHttpClient>();
        services.TryAddScoped<ISonarCloudHttpClient, SonarCloudHttpClient>();
    }
}