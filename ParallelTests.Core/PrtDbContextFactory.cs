using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ParallelRepositoryTests.Repository;

public class PrtDbContextFactory: IDesignTimeDbContextFactory<PrtDbContext>
{
    public PrtDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PrtDbContext>();
        optionsBuilder.UseSqlServer();
        return new PrtDbContext(optionsBuilder.Options);
    }
}