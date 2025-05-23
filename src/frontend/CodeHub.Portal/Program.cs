using CodeHub.Aspire.ServiceDefaults;
using CodeHub.Portal.Extensions;
using CodeHub.Portal.Features;
using MudBlazor.Services;

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

    builder.AddServiceDefaults();
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.RegisterServices(builder.Configuration);
    builder.Services.AddMudServices();
    builder.Services.AddHttpsRedirection(_ => { });

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();
    app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
    await app.RunAsync();
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