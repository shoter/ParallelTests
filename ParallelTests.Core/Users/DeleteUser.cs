using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Users;

public class DeleteUser(PrtDbContext db)
{
    public async Task Delete(Guid id, CancellationToken cancellationToken) => await db.Users.Where(u => u.Id == id)
        .ExecuteDeleteAsync(cancellationToken);
}