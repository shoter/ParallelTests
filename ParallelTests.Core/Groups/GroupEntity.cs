using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParallelRepositoryTests.Repository.Users;

namespace ParallelRepositoryTests.Repository.Groups;

[Table("Group")]
public class GroupEntity
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}