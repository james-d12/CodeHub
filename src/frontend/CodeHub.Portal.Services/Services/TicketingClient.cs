using System.Net.Http.Json;
using System.Text.Json;
using CodeHub.Domain.Ticketing;
using CodeHub.Shared;
using Microsoft.Extensions.Logging;

namespace CodeHub.Portal.Services.Services;

public sealed class TicketingClient : ITicketingClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<TicketingClient> _logger;

    private const string WorkItemUrl = "work-items";

    public TicketingClient(
        HttpClient httpClient,
        JsonSerializerOptions jsonOptions,
        ILogger<TicketingClient> logger)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        _logger = logger;
    }

    public async Task<List<WorkItem>> GetWorkItemsAsync()
    {
        using var activity = Tracing.StartActivity();
        try
        {
            _logger.LogInformation("Getting work items from: {Url}", WorkItemUrl);
            return await _httpClient.GetFromJsonAsync<List<WorkItem>>(WorkItemUrl, _jsonOptions) ?? [];
        }
        catch (Exception exception)
        {
            activity?.RecordException(exception);
            _logger.LogError(exception, "Could not get list of work items from {Url}", WorkItemUrl);
            return [];
        }
    }
}