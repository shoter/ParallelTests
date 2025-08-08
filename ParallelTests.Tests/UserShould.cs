using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ParallelTests.Tests;

public class UserShould(DatabaseFixture fixture)
{
    [Fact]
    public async Task BeCreated()
    {
        var db = await fixture.GetDatabase();
        int a = 123;

    }

}