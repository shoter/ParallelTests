namespace ParallelRepositoryTests.Repository.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Guid GroupId { get; set; }
    public Group? Group { get; set; }
}
