using CodeHub.Api.Jobs;
using CodeHub.Platform.Azure.Extensions;
using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.Soos.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<DiscoveryHostedService>();

builder.Services.RegisterAzure();
builder.Services.RegisterAzureDevOps(builder.Configuration);
builder.Services.RegisterSoos(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontendOrigin",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5231")
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontendOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();