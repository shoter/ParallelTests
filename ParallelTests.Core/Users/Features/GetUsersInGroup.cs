using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Users.Features;

public class GetUsersInGroup(PrtDbContext db)
{
    public async Task<List<UserEntity>> Execute(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await db.UserInGroups
            .Where(g => g.GroupId == groupId)
            .Select(g => g.User)
            .ToListAsync(cancellationToken);
    }
}