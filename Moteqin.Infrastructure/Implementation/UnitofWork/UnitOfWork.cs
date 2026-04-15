using Moteqin.Domain.Interfaces;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoteqinDbContext _context;

        public UnitOfWork(MoteqinDbContext context,
            IRecordingRepository recordings,
            IFeedbackRepository feedbacks,
            IUserProgressRepository userProgresses,
            ISurahRepository surahs,
            IAyahRepository ayahs,
            IGroupRepository groups,
            IGroupMemberRepository groupMembers,
            INotificationRepository notifications,
            IStreakRepository streaks,
            IPointRepository points,
            IDailyPlanRepository dailyPlans)
        {
            _context = context;

            Recordings = recordings;
            Feedbacks = feedbacks;
            UserProgresses = userProgresses;
            Surahs = surahs;
            Ayahs = ayahs;
            Groups = groups;
            GroupMembers = groupMembers;
            Notifications = notifications;
            Streaks = streaks;
            Points = points;
            DailyPlans = dailyPlans;
        }

        public IRecordingRepository Recordings { get; }
        public IFeedbackRepository Feedbacks { get; }
        public IUserProgressRepository UserProgresses { get; }
        public ISurahRepository Surahs { get; }
        public IAyahRepository Ayahs { get; }
        public IGroupRepository Groups { get; }
        public IGroupMemberRepository GroupMembers { get; }
        public INotificationRepository Notifications { get; }
        public IStreakRepository Streaks { get; }
        public IPointRepository Points { get; }
        public IDailyPlanRepository DailyPlans { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}