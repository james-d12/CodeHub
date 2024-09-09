using System.Net.Http.Json;
using CodeHub.Engine.SonarCloud.Models;

namespace CodeHub.Portal.Client.Services;

public sealed class SonarCloudHttpClient(HttpClient httpClient) : ISonarCloudHttpClient
{
    public async Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync()
    {
        try
        {
            var requestUrl = new Uri($"http://localhost:5104/sonarcloud/components");
            var repositories = await httpClient.GetFromJsonAsync<SonarCloudResponse<SonarCloudComponent>>(requestUrl);
            return repositories ?? new SonarCloudResponse<SonarCloudComponent>
            {
                Paging = new SonarCloudPaging
                {
                    PageIndex = 0,
                    PageSize = 0,
                    Total = 0
                },
                Components = []
            };
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return new SonarCloudResponse<SonarCloudComponent>
            {
                Paging = new SonarCloudPaging
                {
                    PageIndex = 0,
                    PageSize = 0,
                    Total = 0
                },
                Components = []
            };
        }
    }
}