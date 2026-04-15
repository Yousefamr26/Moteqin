using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moteqin.Domain.Entity;

public class UserProgressConfiguration : IEntityTypeConfiguration<UserProgress>
{
    public void Configure(EntityTypeBuilder<UserProgress> builder)
    {
        builder.ToTable("UserProgress");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Progresses)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Ayah)
            .WithMany()
            .HasForeignKey(x => x.AyahId);
    }
}