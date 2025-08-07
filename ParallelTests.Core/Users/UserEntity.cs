using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParallelRepositoryTests.Repository.Groups;

namespace ParallelRepositoryTests.Repository.Users;

[Table("User")]
public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}