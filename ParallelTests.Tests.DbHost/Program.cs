using Aspire.Hosting;
using ParallelTests.Tests.DbHost;


var builder = DistributedApplication.CreateBuilder(args);

// SQL Server with a database named "appdb"
var server = builder.AddSqlServer("sqldb")
    .WithContainerName("ParallelTestsSqlServer");

for (int i = 0; i < Const.SqlServerCount; ++i)
{
    server.AddDatabase($"testdb{i}");
}

// Example: your API depends on the DB
builder.Build().Run();