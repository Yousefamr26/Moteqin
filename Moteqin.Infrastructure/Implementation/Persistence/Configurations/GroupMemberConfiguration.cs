using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMember>
{
    public void Configure(EntityTypeBuilder<GroupMember> builder)
    {
        builder.ToTable("GroupMembers");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Group)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.GroupId);

        builder.HasOne(x => x.User)
      .WithMany(x => x.GroupMembers)
      .HasForeignKey(x => x.UserId)
      .OnDelete(DeleteBehavior.Restrict);
    }
}