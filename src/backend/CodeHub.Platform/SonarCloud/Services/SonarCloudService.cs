using System.Net.Http.Headers;
using System.Text.Json;
using CodeHub.Platform.SonarCloud.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.SonarCloud.Services;

internal sealed class SonarCloudService(IOptions<SonarCloudSettings> sonarCloudSettings) : ISonarCloudService
{
    private const string BaseUrl = "https://sonarcloud.io/api";

    private static readonly HttpClient HttpClient = new(new SocketsHttpHandler
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
    });

    public async Task<SonarCloudResponse<SonarCloudComponent>?> GetComponentsAsync(CancellationToken cancellationToken)
    {
        var uri = new Uri($"{BaseUrl}/components/search?organization={sonarCloudSettings.Value.Organization}");
        using HttpRequestMessage request = new(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", sonarCloudSettings.Value.Token);

        try
        {
            using var response = await HttpClient.SendAsync(request, cancellationToken);
            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<SonarCloudResponse<SonarCloudComponent>>(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}