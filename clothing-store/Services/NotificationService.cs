using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task CreateNotificationAsync(string message)
        {
            var notification = new Notification
            {
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task<List<Notification>> GetUnreadNotificationsAsync()
        {
            return await _notificationRepository.GetUnreadNotificationsAsync();
        }
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllNotificationsAsync();
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
        }
    }
}
