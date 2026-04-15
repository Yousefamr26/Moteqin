using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserPointsConfiguration : IEntityTypeConfiguration<UserPoints>
{
    public void Configure(EntityTypeBuilder<UserPoints> builder)
    {
        builder.ToTable("UserPoints");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalPoints)
            .HasDefaultValue(0);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict); // 🔥 مهم جدًا
    }
}