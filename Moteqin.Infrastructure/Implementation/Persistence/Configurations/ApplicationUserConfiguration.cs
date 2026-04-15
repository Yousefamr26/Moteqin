using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasMany(x => x.Progresses)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Recordings)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Feedbacks)
     .WithOne(x => x.Sheikh)
     .HasForeignKey(x => x.SheikhId)
     .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.GroupMembers)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Points)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Streak)
            .WithOne(x => x.User)
            .HasForeignKey<Streak>(x => x.UserId);

        builder.HasMany(x => x.Notifications)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}