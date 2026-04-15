public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetUserNotificationsAsync(string userId);
    Task MarkAsReadAsync(int notificationId);
}