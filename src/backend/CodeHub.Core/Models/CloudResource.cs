namespace CodeHub.Engine.Models;

public enum CloudPlatform
{
    Azure,
    Aws,
    GoogleCloud
}

public abstract class CloudResource
{
    public required string Id { get; set; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Url { get; init; }
    public CloudPlatform Platform { get; init; }

    protected CloudResource(string id, string name, string description, string url, CloudPlatform platform)
    {
        Id = id;
        Name = name;
        Description = description;
        Url = url;
        Platform = platform;
    }
}