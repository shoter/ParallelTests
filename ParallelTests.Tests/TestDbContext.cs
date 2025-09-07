using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository;
using ParallelRepositoryTests.Repository.Groups;
using ParallelRepositoryTests.Repository.Users;

namespace ParallelTests.Tests;

public class TestDbContext(DbContextOptions<PrtDbContext> options) : PrtDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasData(
                SampleData.UserWithoutGroup,
                SampleData.UserWithinGroup);

        modelBuilder.Entity<GroupEntity>()
            .HasData(
                SampleData.GroupWithUser,
                SampleData.UserWithinGroup);

        modelBuilder.Entity<UserInGroupEntity>()
            .HasData(SampleData.UserInGroup);
    }
}