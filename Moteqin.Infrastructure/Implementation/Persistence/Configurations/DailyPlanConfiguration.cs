using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DailyPlanConfiguration : IEntityTypeConfiguration<DailyPlan>
{
    public void Configure(EntityTypeBuilder<DailyPlan> builder)
    {
        builder.ToTable("DailyPlans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.IsCompleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}