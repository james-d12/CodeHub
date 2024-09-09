using System.Text.Json;
using CodeHub.Engine.SooS.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Engine.SooS.Services;

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

    public Task<List<SoosProject>> GetScansAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}