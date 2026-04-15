using System.Threading.Tasks;

namespace Moteqin.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRecordingRepository Recordings { get; }
        IFeedbackRepository Feedbacks { get; }
        IUserProgressRepository UserProgresses { get; }
        ISurahRepository Surahs { get; }
        IAyahRepository Ayahs { get; }
        IGroupRepository Groups { get; }
        IGroupMemberRepository GroupMembers { get; }
        INotificationRepository Notifications { get; }
        IStreakRepository Streaks { get; }
        IPointRepository Points { get; }
        IDailyPlanRepository DailyPlans { get; }

        Task<int> SaveChangesAsync();
    }
}