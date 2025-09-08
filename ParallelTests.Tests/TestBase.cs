using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ParallelRepositoryTests.Repository;

namespace ParallelTests.Tests;

public class TestBase : IDisposable
{
    private DatabaseFixture fixture;

    public static CancellationToken CT => TestContext.Current.CancellationToken;
    internal IFixture fix = new Fixture();
    private DatabaseFixture.DatabaseResource dbResource;
    private IDbContextTransaction transaction;
    internal PrtDbContext db;

    public TestBase(DatabaseFixture fixture)
    {
        this.fixture = fixture;
        dbResource = fixture.GetDatabase(CT).Result;

        var options = new DbContextOptionsBuilder<PrtDbContext>()
            .UseSqlServer(dbResource.ConnectionString)
            .Options;

        db = new TestDbContext(options);
        transaction = db.Database.BeginTransaction();
    }


    internal PrtDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<PrtDbContext>()
            .UseSqlServer(dbResource.ConnectionString)
            .Options;

        return new TestDbContext(options);
    }


    public void Dispose()
    {
        transaction.Rollback();
        transaction.Dispose();
        db.Dispose();
        dbResource.Dispose();
    }
}