using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParallelRepositoryTests.Repository.Groups;

namespace ParallelRepositoryTests.Repository.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public Guid GroupId { get; set; }
    public GroupEntity? Group { get; set; }
}