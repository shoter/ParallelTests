using Microsoft.EntityFrameworkCore;
using ParallelRepositoryTests.Repository.Entities;

namespace ParallelRepositoryTests.Repository.Users;

public class GetUser(PrtDbContext db)
{
    public async Task<UserEntity?> Execute(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

}