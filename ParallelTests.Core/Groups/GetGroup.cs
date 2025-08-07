using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Groups;

public class GetGroup(PrtDbContext db)
{
    public async Task<GroupEntity?> Execute(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Groups.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}
