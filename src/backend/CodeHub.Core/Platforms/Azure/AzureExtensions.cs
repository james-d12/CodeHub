﻿using CodeHub.Core.Models;
using CodeHub.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.Azure;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.TryAddTransient<IAzureService, AzureService>();
        services.TryAddKeyedTransient<IDiscoveryService, AzureDiscoveryService>(DiscoveryServiceType.Azure);
        return services;
    }
}