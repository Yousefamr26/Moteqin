using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moteqin.Domain.Entity;

public class SurahConfiguration : IEntityTypeConfiguration<Surah>
{
    public void Configure(EntityTypeBuilder<Surah> builder)
    {
        builder.ToTable("Surahs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Ayahs)
            .WithOne(x => x.Surah)
            .HasForeignKey(x => x.SurahId);
    }
}