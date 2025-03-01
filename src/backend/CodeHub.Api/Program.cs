using System.Text.Json;
using CodeHub.Api.Jobs;
using CodeHub.Api.Settings;
using CodeHub.Platform.Azure.Extensions;
using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.GitHub.Extensions;
using CodeHub.Platform.GitLab.Extensions;
using Microsoft.VisualStudio.Services.Common;

var builder = WebApplication.CreateBuilder(args);
var applicationName = AppDomain.CurrentDomain.FriendlyName;

var loggerFactory = LoggerFactory.Create(loggingBuilder =>
{
    loggingBuilder.AddJsonConsole();
    loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingBuilder.AddDebug();
    loggingBuilder.AddEventSourceLogger();
});
var logger = loggerFactory.CreateLogger<Program>();

try
{
    logger.LogDebug("Configuration: {Config}", JsonSerializer.Serialize(builder.Configuration.GetSection("CorsSettings").Value));
    logger.LogInformation("Starting up: {ApplicationName}", applicationName);

    builder.Services.AddLogging();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHostedService<DiscoveryHostedService>();

    builder.Services.RegisterAzure(builder.Configuration);
    builder.Services.RegisterAzureDevOps(builder.Configuration);
    builder.Services.RegisterGitHub(builder.Configuration);
    builder.Services.RegisterGitLab(builder.Configuration);
    
    var corsSettings = builder.Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>();

    builder.Services.AddCors(options =>
    {
        logger.LogInformation("Enabling Cors.");

        corsSettings?.Policies
            .ForEach(policy =>
            {
                logger.LogInformation("Adding Cors policy: {Name} {Origin}", policy.Key, policy.Value);

                options.AddPolicy(name: policy.Key, corsPolicy =>
                {
                    corsPolicy
                        .WithOrigins(policy.Value)
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        logger.LogInformation("Adding swagger UI");
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowFrontendOrigin");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.LogCritical(exception, "Could not startup: {ApplicationName}.", applicationName);
    throw;
}
finally
{
    logger.LogInformation("Stopping: {ApplicationName}.", applicationName);
}