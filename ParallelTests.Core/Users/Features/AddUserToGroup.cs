using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace ParallelRepositoryTests.Repository.Users.Features;

public class AddUserToGroup(PrtDbContext db)
{
    public record Input(
        Guid UserId,
        Guid GroupId);

    public async Task Execute(Input input, CancellationToken cancellationToken = default)
    {
        bool exists = await db.UserInGroups
            .AnyAsync(ug => ug.UserId == input.UserId && ug.GroupId == input.GroupId,
                cancellationToken);
        if (exists)
        {
            return;
        }

        if (!exists)
        {
            var entity = new UserInGroupEntity()
            {
                UserId = input.UserId,
                GroupId = input.GroupId
            };

            await db.AddAsync(entity, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}