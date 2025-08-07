using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository.Entities;

namespace ParallelRepositoryTests.Repository;

public class PrtDbContext : DbContext
{
    public PrtDbContext(DbContextOptions<PrtDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Group> Groups => Set<Group>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).HasMaxLength(200);

            entity
                .HasOne(u => u.Group)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
