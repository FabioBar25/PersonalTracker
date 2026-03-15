using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

// Database Config
var sqlPassword = builder.AddParameter("sqlPassword", secret: true);
var sql = builder.AddSqlServer("sqlserver", sqlPassword)
                 .WithDataVolume();

var db = sql.AddDatabase("development");

// Add ASP.NET Core API and reference the database or BACK END API
var apiService = builder.AddProject<Projects.Host_Api>("PerTrack-host-api");
apiService
    .WithReference(db, "SqlAzure")
    .WaitFor(db)
    .WithCommand(
        name: "scalar-docs",
        displayName: "Scalar Docs",
        executeCommand: async (ExecuteCommandContext context) =>
        {
            var endpoint = apiService.GetEndpoint("https");

            var url = $"{endpoint.Url}/scalar";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            await Task.CompletedTask;
            return CommandResults.Success();
        },
        commandOptions: new CommandOptions
        {
            UpdateState = context => ResourceCommandState.Enabled,
            Description = "Scalar Docs",
            IconName = "DocumentTextLink",
            IconVariant = IconVariant.Filled,
            IsHighlighted = false,
        }
    );

var proxy = builder.AddProject<Projects.Host_Proxy>("Pertrack-host-proxy");
proxy.WaitFor(apiService)
    .WithCommand(
        name: "scalar-docs",
        displayName: "Scalar Docs",
        executeCommand: async (ExecuteCommandContext context) =>
        {
            var endpoint = proxy.GetEndpoint("https");

            var url = $"{endpoint.Url}/scalar";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            await Task.CompletedTask;
            return CommandResults.Success();
        },
        commandOptions: new CommandOptions
        {
            UpdateState = context => ResourceCommandState.Enabled,
            Description = "Scalar Docs",
            IconName = "DocumentTextLink",
            IconVariant = IconVariant.Filled,
            IsHighlighted = false,
        }
    );


//The Angular front end
builder.AddNpmApp("personalTracker-client", "C:/Users/joako/OneDrive/Documents/GitHub/PersonalTracker/Personal-Tracker-Front")
    .WaitFor(proxy);

builder.Build().Run();