using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IUserEventRepository
    {
        Task AddAsync(UserEvent userEvent);
        Task<int> GetDailyVisitCountAsync(DateTime date);
        Task<int> GetTotalVisitCountAsync();
    }
}

