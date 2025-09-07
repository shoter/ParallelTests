using ParallelRepositoryTests.Repository.Groups;
using ParallelRepositoryTests.Repository.Users;

namespace ParallelTests.Tests;

public static class SampleData
{
    public static UserEntity UserWithoutGroup { get; } = new()
    {
        Id = Guid.Parse("241af630-c555-4375-8f17-b45ad39953d1"),
        Name = "User without group"
    };

    public static UserEntity UserWithinGroup { get; } = new()
    {
        Id = Guid.Parse("2590a63a-99ec-4051-bbd6-9dff5f76d85f"),
        Name = "User in group"
    };

    public static GroupEntity GroupWithoutUsers { get; } = new GroupEntity()
    {
        Id = Guid.Parse("cc0c2bf9-49d3-48e5-8002-4bd002e24813"),
        Name = "Group without users"
    };

    public static GroupEntity GroupWithUser { get; } = new GroupEntity()
    {
        Id = Guid.Parse("c55d523e-bca6-4d9e-9139-12b38fb7dfb0"),
        Name = "Group with user"
    };

    public static UserInGroupEntity UserInGroup { get; set; }

    static SampleData()
    {
        UserInGroup = new()
        {
            GroupId = GroupWithUser.Id,
            UserId = UserWithinGroup.Id,
        };
    }
}