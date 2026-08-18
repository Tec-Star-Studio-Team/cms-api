var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("cms");

builder.AddProject<Projects.CmsApi_Server>("server")
    .WithReference(sqlServer)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

builder.Build().Run();
