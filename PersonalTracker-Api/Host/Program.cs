var builder = WebApplication.CreateBuilder(args);

// Aspire defaults (logging, telemetry, health checks)
//builder.AddServiceDefaults();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

// Aspire health checks
//app.MapDefaultEndpoints();

app.Run();