using CodeHub.Portal.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Portal.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var codeHubBackendBaseUrl = configuration.GetValue<string>("CodeHubBackendBaseUrl");

        if (string.IsNullOrEmpty(codeHubBackendBaseUrl))
        {
            throw new ArgumentException("CodeHubBackendBaseUrl cannot be null or empty.");
        }

        if (!Uri.IsWellFormedUriString(codeHubBackendBaseUrl, UriKind.Absolute))
        {
            throw new ArgumentException("CodeHubBackendBaseUrl is not a valid URL.");
        }

        services.AddHttpClient<IAzureDevOpsHttpClient, AzureDevOpsHttpClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/azure-devops");
        });

        services.AddHttpClient<IAzureHttpClient, AzureHttpClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/azure");
        });

        services.AddHttpClient<IResourceHttpClient, ResourceHttpClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/resources/");
        });

        services.TryAddTransient<IAzureDevOpsHttpClient, AzureDevOpsHttpClient>();
        services.TryAddTransient<IAzureHttpClient, AzureHttpClient>();
        services.TryAddTransient<IResourceHttpClient, ResourceHttpClient>();
    }
}