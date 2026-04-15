using Microsoft.EntityFrameworkCore;
using Moteqin.Domain.Entity;
using Moteqin.Infrastructure.Implementation.Persistence.Conetxt;

namespace Moteqin.Infrastructure.Implementation.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(MoteqinDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notif = await _dbSet.FindAsync(notificationId);
            if (notif != null)
            {
                notif.IsRead = true;
                _dbSet.Update(notif);
            }
        }
    }
}