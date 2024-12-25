using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string message);
        Task<List<Notification>> GetUnreadNotificationsAsync();
        Task MarkNotificationAsReadAsync(int notificationId);
        Task<List<Notification>> GetAllNotificationsAsync();

    }
}
