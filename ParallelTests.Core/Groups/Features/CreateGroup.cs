namespace ParallelRepositoryTests.Repository.Groups.Features;

public class CreateGroup(PrtDbContext db)
{
    public record Input(
        Guid Id,
        string Name);

    public async Task Execute(Input input, CancellationToken cancellationToken = default)
    {
        var group = new GroupEntity()
        {
            Id = input.Id,
            Name = input.Name
        };
        await db.AddAsync(group, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}