using System.Collections.Concurrent;
using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository;
using ParallelTests.Tests;
using ParallelTests.Tests.DbHost;
using Xunit.Sdk;

[assembly: AssemblyFixture(typeof(DatabaseFixture))]

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

        app = builder
            .BuildAsync()
            .Result;
        app.StartAsync()
            .Wait();

        for (int i = 0; i < Const.SqlServerCount; ++i)
        {
            var dbInfo = new DatabaseInfo(i,
                app.GetConnectionStringAsync($"testdb{i}")
                    .Result ?? throw new NullReferenceException("DB not found"));

            var options = new DbContextOptionsBuilder<PrtDbContext>()
                .UseSqlServer(dbInfo.ConnectionString)
                .Options;

            using var db = new TestDbContext(options);
            while (!db.Database.CanConnect())
            {
                Thread.Sleep(1000);
            }

            db.Database.EnsureCreated();

            databases.Enqueue(dbInfo);
        }
    }

    internal async Task<DatabaseResource> GetDatabase(CancellationToken ct = default)
    {
        await semaphoreSlim.WaitAsync(ct);
        if (!databases.TryDequeue(out var dbInfo))
        {
            throw new Exception("Should not happen - semaphore guards against that");
        }

        Console.WriteLine($"O {dbInfo.DbNumber} {databases.Count}");
        return new(this, dbInfo);
    }

    private void Release(DatabaseInfo dbInfo)
    {
        databases.Enqueue(dbInfo);
        semaphoreSlim.Release();
    }

    internal class DatabaseResource(
        DatabaseFixture fixture,
        DatabaseInfo dbInfo) : IDisposable
    {
        public string ConnectionString => dbInfo.ConnectionString;

        public int DbNumber => dbInfo.DbNumber;

        public void Dispose()
        {
            fixture.Release(dbInfo);
        }
    }
}