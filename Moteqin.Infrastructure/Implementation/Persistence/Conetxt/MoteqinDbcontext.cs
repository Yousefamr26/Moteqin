using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Common;
using Moteqin.Domain.Entity;

namespace Moteqin.Infrastructure.Implementation.Persistence.Conetxt
{
    public class MoteqinDbContext : IdentityDbContext<ApplicationUser>
    {
        public MoteqinDbContext(DbContextOptions<MoteqinDbContext> options)
            : base(options)
        {
        }

        public DbSet<Surah> Surahs { get; set; }
        public DbSet<Ayah> Ayahs { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<DailyPlan> DailyPlans { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Streak> Streaks { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MoteqinDbContext).Assembly);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var createdAt = entityType.FindProperty(nameof(BaseEntity.CreatedAt));
                    if (createdAt != null)
                        createdAt.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd;

                    var updatedAt = entityType.FindProperty(nameof(BaseEntity.UpdatedAt));
                    if (updatedAt != null)
                        updatedAt.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate;
                }
            }
        }
    }
}