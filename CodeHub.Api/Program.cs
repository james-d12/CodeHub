using CodeHub.Engine.Azure;
using CodeHub.Engine.AzureDevOps;
using CodeHub.Engine.SonarCloud;
using CodeHub.Engine.Soos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterSonarCloudServices(builder.Configuration);
builder.Services.RegisterAzureDevOpsServices(builder.Configuration);
builder.Services.RegisterSoosServices(builder.Configuration);
builder.Services.RegisterAzureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();