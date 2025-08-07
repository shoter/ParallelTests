using ParallelRepositoryTests.Repository.Entities;

namespace ParallelRepositoryTests.Repository.Users;

public class CreateUser(PrtDbContext db)
{
    public record Input(
        Guid Id,
        string Name);

    public async Task Execute(Input input, CancellationToken cancellationToken = default)
    {
        var user = new UserEntity()
        {
            Id = input.Id,
            Name = input.Name
        };
        await db.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}