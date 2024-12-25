using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IUserEventService
    {
        Task LogEventAsync(UserEvent userEvent);
        Task<int> GetDailyVisitsAsync(DateTime date);
        Task<int> GetTotalVisitsAsync();
    }
}
