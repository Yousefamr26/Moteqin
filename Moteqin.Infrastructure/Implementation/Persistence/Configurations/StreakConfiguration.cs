using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StreakConfiguration : IEntityTypeConfiguration<Streak>
{
    public void Configure(EntityTypeBuilder<Streak> builder)
    {
        builder.ToTable("Streaks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CurrentStreak).HasDefaultValue(0);
        builder.Property(x => x.LongestStreak).HasDefaultValue(0);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Streak)
            .HasForeignKey<Streak>(x => x.UserId);
    }
}