using Aspire.Hosting;
using ParallelTests.Tests.DbHost;


var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter(
    "password",
    secret: false,
    value: "IsItAPasswordProblem0!");

// SQL Server with a database named "appdb"
var server = builder.AddSqlServer("sqldb", password)
    .WithContainerName("ParallelTestsSQL")
    .WithLifetime(ContainerLifetime.Persistent);

for (int i = 0; i < Const.SqlServerCount; ++i)
{
    server.AddDatabase($"testdb{i}");
}

// Example: your API depends on the D

builder.Build().Run();