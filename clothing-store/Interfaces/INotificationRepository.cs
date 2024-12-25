using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        Task<List<Notification>> GetUnreadNotificationsAsync();
        Task MarkAsReadAsync(int notificationId);
        Task<List<Notification>> GetAllNotificationsAsync();

    }
}

