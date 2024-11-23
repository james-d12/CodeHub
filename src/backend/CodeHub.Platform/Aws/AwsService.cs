using Amazon.ResourceGroupsTaggingAPI;
using Amazon.ResourceGroupsTaggingAPI.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.Aws;

internal sealed class AwsService
{
    private readonly AmazonResourceGroupsTaggingAPIClient _amazonResourceGroupsTaggingApiClient;

    public AwsService(IOptions<AwsSettings> options)
    {
        var credentials = new BasicAWSCredentials(options.Value.AccessKey, options.Value.SecretKey);
        _amazonResourceGroupsTaggingApiClient = new AmazonResourceGroupsTaggingAPIClient(credentials);
    }

    public async Task GetAllResources()
    {
        try
        {
            var request = new GetResourcesRequest();
            var resources = await _amazonResourceGroupsTaggingApiClient.GetResourcesAsync(request);

            foreach (var resourceTagMapping in resources.ResourceTagMappingList)
            {
                foreach (var tag in resourceTagMapping.Tags)
                {
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}