var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject("api", "../CodeHub.Api");
builder.AddProject("portal", "../CodeHub.Portal").WithReference(api);

builder.Build().Run();