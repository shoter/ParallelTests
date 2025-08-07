using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Users.Features;

public class GetUser(PrtDbContext db)
{
    public async Task<UserEntity?> Execute(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

}