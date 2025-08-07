using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParallelRepositoryTests.Repository.Groups;

namespace ParallelRepositoryTests.Repository.Users;

[Table("UserInGroup")]
public class UserInGroupEntity : IEntityTypeConfiguration<UserInGroupEntity>
{
    public UserEntity User { get; set; } = null!;
    public Guid UserId { get; set; }
    public GroupEntity Group { get; set; } = null!;
    public Guid GroupId { get; set; }

    public void Configure(EntityTypeBuilder<UserInGroupEntity> builder)
    {
        builder.HasKey(x => new
        {
            x.UserId,
            x.GroupId
        });

        builder.HasIndex(x => new
        {
            x.GroupId,
            x.UserId
        });
    }
}