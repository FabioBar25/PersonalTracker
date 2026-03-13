using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sqlPassword", AccessCommon.Constants.DevPassword);
var sql = builder.AddSqlServer("sqlserver", sqlPassword)
                 .WithDataVolume();

var db = sql.AddDatabase("Ppy-Development");

// Azure Storage
var blobs = builder
    .AddAzureStorage("storage")
    .RunAsEmulator()
    .AddBlobs("blobs");


// Add ASP.NET Core API and reference the database
var apiService = builder.AddProject<Projects.Host_Api>("ppy-host-api");
apiService
    .WithReference(db, "SqlAzure")
    .WaitFor(db)
    .WithReference(blobs, "AzureStorage")
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

var proxy = builder.AddProject<Projects.Host_Proxy>("ppy-host-proxy");
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

builder.AddNpmApp("ppy-client", "../../../abstra-workspace")
    .WaitFor(proxy);

builder.Build().Run();