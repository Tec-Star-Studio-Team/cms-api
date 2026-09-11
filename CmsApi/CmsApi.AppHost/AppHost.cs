var builder = DistributedApplication.CreateBuilder(args);

// Create the parameter first — reads from user-secrets
var sqlPassword = builder.AddParameter("cmsdb-password", secret: true);

var sqlServer = builder
    .AddSqlServer("sql-server-cms-api", password: sqlPassword)
    .WithDataVolume()
    .WithHostPort(1433)
    .AddDatabase("cmsdb", databaseName: "cmsapi");


builder.AddProject<Projects.CmsApi_Server>("server")
    .WaitFor(sqlServer)
    .WithReference(sqlServer)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

builder.Build().Run();
