using CodeHub.Portal.Services.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.RegisterServices();
builder.Services.AddMudServices();

await builder.Build().RunAsync();