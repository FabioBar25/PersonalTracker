using Microsoft.Extensions.Hosting;
using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// builder.AddServiceDefaults();

// Add Azure Monitor for Telemetry
builder.Services.AddOpenTelemetry().UseAzureMonitor();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();
app.UseHttpsRedirection();

await app.RunAsync();