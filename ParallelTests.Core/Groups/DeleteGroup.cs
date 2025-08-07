using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Groups;

public class DeleteGroup(PrtDbContext db)
{
    public async Task Execute(Guid id, CancellationToken cancellationToken) => await db.Groups.Where(g => g.Id == id)
        .ExecuteDeleteAsync(cancellationToken);
}