using Aspire.Hosting;
using Aspire.Hosting.Testing;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ParallelRepositoryTests.Repository.Users;

namespace ParallelTests.Tests;

public class UserShould(DatabaseFixture fixture) : TestBase(fixture)
{
    [Theory]
    [MemberData(nameof(GetData),  10_000)]
    public async Task BeCreated(int dummy)
    {
        var user = fix.Create<UserEntity>();
        user.Name = $"Something{dummy}";
        await db.Users.AddAsync(user, CT);
        await db.SaveChangesAsync(CT);
        var retrievedUser = await db.Users.FirstAsync(x => x.Id == user.Id, CT);
        Assert.Equivalent(user, retrievedUser);
    }

    public static IEnumerable<object[]> GetData(int numTests)
    {
        object[][] arr = new object[numTests][];
        for (int i = 0; i < numTests; ++i)
        {
            arr[i] = new object[] { i };
        }

        return arr;
    }


}