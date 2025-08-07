using System.ComponentModel.DataAnnotations;
using ParallelRepositoryTests.Repository.Entities;

namespace ParallelRepositoryTests.Repository.Groups;

public class GroupEntity
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}