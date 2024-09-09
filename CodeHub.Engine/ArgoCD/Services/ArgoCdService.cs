using System.Text.Json;
using CodeHub.Engine.ArgoCD.Models.Application;

namespace CodeHub.Engine.ArgoCD.Services;

internal sealed class ArgoCdService(string instanceName) : IArgoCdService
{
    private static readonly HttpClient _httpClient = new(new SocketsHttpHandler()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
    });

    public async Task<Application?> GetApplication(string cookie)
    {
        var uri = new Uri($"{instanceName}/api/v1/applications");
        using HttpRequestMessage request = new(HttpMethod.Get, uri);

        request.Headers.Add("Cookie", cookie);

        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        var jsonString = await response.Content.ReadAsStringAsync();
        var application = JsonSerializer.Deserialize<Application>(jsonString);

        foreach (var item in application?.Items ?? [])
        {
            Console.WriteLine("Item Name: {0}", item.MetaData.Name);
        }

        return application;
    }
}