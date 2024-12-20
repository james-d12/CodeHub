﻿using CodeHub.Platform.Azure.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureHttpClient
{
    Task<List<AzureResource>> GetResourcesAsync();
    Task<List<AzureSubscription>> GetSubscriptionsAsync();
}