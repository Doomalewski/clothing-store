using clothing_store.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return View(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            TempData["Message"] = "Powiadomienie oznaczone jako przeczytane.";
            return RedirectToAction(nameof(Index));
        }
    }
}
