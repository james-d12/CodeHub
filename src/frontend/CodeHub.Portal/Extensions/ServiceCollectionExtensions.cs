using System.Text.Json;
using System.Text.Json.Serialization;
using CodeHub.Portal.Features.Cloud;
using CodeHub.Portal.Features.Git.AzureDevOps;
using CodeHub.Portal.Features.Git.Client;
using CodeHub.Portal.Features.Ticketing;
using CodeHub.Shared;

namespace CodeHub.Portal.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        using var activity = Tracing.StartActivity();
        var codeHubBackendBaseUrl = configuration.GetValue<string>("CodeHubBackendBaseUrl");

        if (string.IsNullOrEmpty(codeHubBackendBaseUrl))
        {
            throw new ArgumentException("CodeHubBackendBaseUrl cannot be null or empty.");
        }

        if (!Uri.IsWellFormedUriString(codeHubBackendBaseUrl, UriKind.Absolute))
        {
            throw new ArgumentException("CodeHubBackendBaseUrl is not a valid URL.");
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        services.AddSingleton(jsonOptions);

        services.AddHttpClient<ICloudHttpClient, CloudHttpClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/cloud/");
        });

        services.AddHttpClient<IGitHttpClient, GitHttpClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/git/");
        });

        services.AddHttpClient<ITicketingClient, TicketingClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/ticketing/");
        });

        services.AddHttpClient<IAzureDevOpsClient, AzureDevOpsClient>(client =>
        {
            client.BaseAddress = new Uri($"{codeHubBackendBaseUrl}/azure-devops/");
        });
    }
}