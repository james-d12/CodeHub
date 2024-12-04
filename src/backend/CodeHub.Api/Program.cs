using CodeHub.Api.Jobs;
using CodeHub.Platform.Azure.Extensions;
using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.Soos.Extensions;

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
    logger.LogInformation("Starting up: {ApplicationName}", applicationName);
    builder.Services.AddLogging();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHostedService<DiscoveryHostedService>();

    builder.Services.RegisterAzure(builder.Configuration);
    builder.Services.RegisterAzureDevOps(builder.Configuration);
    builder.Services.RegisterSoos(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowFrontendOrigin",
            policy =>
            {
                policy
                    .WithOrigins("http://localhost:5231")
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
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