using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moteqin.Domain.Entity;

public class AyahConfiguration : IEntityTypeConfiguration<Ayah>
{
    public void Configure(EntityTypeBuilder<Ayah> builder)
    {
        builder.ToTable("Ayahs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AyahNumber)
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired();

        builder.HasOne(x => x.Surah)
            .WithMany(x => x.Ayahs)
            .HasForeignKey(x => x.SurahId);
    }
}