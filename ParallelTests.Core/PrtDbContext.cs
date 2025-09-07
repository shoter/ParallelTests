using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository.Groups;
using ParallelRepositoryTests.Repository.Users;

namespace ParallelRepositoryTests.Repository;

public class PrtDbContext(DbContextOptions<PrtDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<GroupEntity> Groups => Set<GroupEntity>();
    public DbSet<UserInGroupEntity> UserInGroups => Set<UserInGroupEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(UserEntity)
                .Assembly);
    }
}