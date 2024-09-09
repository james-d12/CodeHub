using CodeHub.Portal.Client.Services;
using MudBlazor.Services;
using CodeHub.Portal.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ISonarCloudHttpClient, SonarCloudHttpClient>();
builder.Services.AddScoped<IAzureHttpClient, AzureHttpClient>();
builder.Services.AddScoped<IAzureDevOpsHttpClient, AzureDevOpsHttpClient>();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CodeHub.Portal.Client._Imports).Assembly);

await app.RunAsync();