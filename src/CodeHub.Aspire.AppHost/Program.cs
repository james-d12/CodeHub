var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.CodeHub_Api>("api");
builder.AddProject<Projects.CodeHub_Portal>("portal")
    .WithReference(api);

builder.Build().Run();