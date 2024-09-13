using System.Text.Json;
using CodeHub.Platform.SooS.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.SooS.Services;

internal sealed class SoosService(IOptions<SoosSettings> soosSettings, ISoosCacheService soosCacheService)
    : ISoosService
{
    private static readonly HttpClient HttpClient = new(new SocketsHttpHandler()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
    });

    public async Task<List<SoosProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        var cachedProjects = soosCacheService.GetProjects();

        if (cachedProjects.Count >= 1)
        {
            return cachedProjects;
        }

        var uri = new Uri(SoosConstants.GetProjectsUrl(soosSettings.Value.ClientId));
        using HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add(SoosConstants.ApiKeyHeaderName, soosSettings.Value.Key);

        try
        {
            using var response = await HttpClient.SendAsync(request, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var projects = JsonSerializer.Deserialize<List<SoosProject>>(jsonString) ?? [];

            soosCacheService.SetProjects(projects);

            return projects;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<List<SoosProjectBranch>> GetProjectBranchesAsync(string projectId, CancellationToken cancellationToken)
    {
        var uri = new Uri(SoosConstants.GetProjectBranchesUrl(soosSettings.Value.ClientId, projectId));
        using HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add(SoosConstants.ApiKeyHeaderName, soosSettings.Value.Key);

        try
        {
            using var response = await HttpClient.SendAsync(request, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var branches = JsonSerializer.Deserialize<List<SoosProjectBranch>>(jsonString) ?? [];
            return branches;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<SoosProjectSetting?> GetProjectSettingsAsync(string projectId, CancellationToken cancellationToken)
    {
        var uri = new Uri(SoosConstants.GetProjectSettingsUrl(soosSettings.Value.ClientId, projectId));
        using HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Add(SoosConstants.ApiKeyHeaderName, soosSettings.Value.Key);

        try
        {
            using var response = await HttpClient.SendAsync(request, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var projectSettings = JsonSerializer.Deserialize<SoosProjectSetting>(jsonString);
            return projectSettings;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

}