using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.ToTable("Points");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Points)
            .HasForeignKey(x => x.UserId);
    }
}