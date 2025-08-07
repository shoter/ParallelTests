using System.Collections.Concurrent;
using Aspire.Hosting.Testing;
using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository;
using ParallelTests.Tests.DbHost;
using Xunit.Sdk;

namespace ParallelTests.Tests;

public class DatabaseFixture
{
    private readonly DistributedApplication app;
    private ConcurrentQueue<DatabaseInfo> databases = new ConcurrentQueue<DatabaseInfo>();
    private readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(Const.SqlServerCount, Const.SqlServerCount);

    public DatabaseFixture()
    {
        var builder = DistributedApplicationTestingBuilder
            .CreateAsync<Projects.ParallelTests_Tests_DbHost>()
            .Result;

        app = builder.BuildAsync()
            .Result;
        app.StartAsync()
            .Wait();

        for (int i = 0; i < Const.SqlServerCount; ++i)
        {
            var dbInfo = new DatabaseInfo(
                app.GetConnectionStringAsync($"testdb{i}")
                    .Result ?? throw new NullReferenceException("DB not found"));

            var options = new DbContextOptionsBuilder<PrtDbContext>()
                .UseSqlServer(dbInfo.ConnectionString)
                .Options;

            using var db = new PrtDbContext(options);
            db.Database.Migrate();

            databases.Enqueue(dbInfo);
        }
    }

    private async Task<DatabaseResource> GetDatabase(CancellationToken ct)
    {
        await semaphoreSlim.WaitAsync(ct);
        if (!databases.TryDequeue(out var dbInfo))
        {
            throw new Exception("Should not happen - semaphore guards against that");
        }

        return new(this, dbInfo.ConnectionString);
    }

    private void Release()
    {
        semaphoreSlim.Release();
    }

    internal class DatabaseResource(
        DatabaseFixture fixture,
        string connectionString) : IDisposable
    {
        public string ConnectionString => connectionString;

        public void Dispose()
        {
            fixture.Release();
        }
    }
}