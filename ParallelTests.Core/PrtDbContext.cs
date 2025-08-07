using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository.Entities;
using ParallelRepositoryTests.Repository.Groups;

namespace ParallelRepositoryTests.Repository;

public class PrtDbContext(DbContextOptions<PrtDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<GroupEntity> Groups => Set<GroupEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}