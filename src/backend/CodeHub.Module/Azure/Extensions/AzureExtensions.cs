﻿using CodeHub.Domain.Cloud.Service;
using CodeHub.Domain.Discovery;
using CodeHub.Module.Azure.Models;
using CodeHub.Module.Azure.Services;
using CodeHub.Module.Azure.Validation;
using CodeHub.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Module.Azure.Extensions;

public static class AzureExtensions
{
    public static void RegisterAzure(this IServiceCollection services, IConfiguration configuration)
    {
        using var activity = Tracing.StartActivity();
        var settings = AzureSettingsValidator.GetValidSettings(configuration);

        if (!settings.IsEnabled)
        {
            return;
        }

        services.RegisterServices();
        services.RegisterCache();
        services.RegisterOptions(configuration);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureService, AzureService>();
        services.AddScoped<ICloudQueryService, AzureCloudQueryService>();
        services.AddSingleton<IDiscoveryService, AzureDiscoveryService>();
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AzureSettings>()
            .Bind(configuration.GetRequiredSection(nameof(AzureSettings)));
    }
}