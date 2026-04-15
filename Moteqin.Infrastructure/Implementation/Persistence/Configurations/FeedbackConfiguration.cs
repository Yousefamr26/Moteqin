using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CommentText)
            .HasMaxLength(500);

        builder.HasOne(x => x.Recording)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.RecordingId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Sheikh)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.SheikhId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}