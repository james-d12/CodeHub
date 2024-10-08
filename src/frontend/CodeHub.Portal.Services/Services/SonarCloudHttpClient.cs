using System.Net.Http.Json;
using CodeHub.Core.Platforms.SonarCloud;

namespace CodeHub.Portal.Services.Services;

public sealed class SonarCloudHttpClient(HttpClient httpClient) : ISonarCloudHttpClient
{
    public async Task<SonarCloudResponse<SonarCloudComponent>> GetComponentsAsync()
    {
        try
        {
            var repositories = await httpClient.GetFromJsonAsync<SonarCloudResponse<SonarCloudComponent>>("components");
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