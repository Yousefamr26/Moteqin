using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecordingConfiguration : IEntityTypeConfiguration<Recording>
{
    public void Configure(EntityTypeBuilder<Recording> builder)
    {
        builder.ToTable("Recordings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileUrl)
            .IsRequired();

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Recordings)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Feedbacks)
            .WithOne(x => x.Recording)
            .HasForeignKey(x => x.RecordingId)
            .OnDelete(DeleteBehavior.NoAction); ;
    }
}