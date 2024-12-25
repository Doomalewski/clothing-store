using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;

        public UserEventService(IUserEventRepository userEventRepository)
        {
            _userEventRepository = userEventRepository;
        }

        public async Task LogEventAsync(UserEvent userEvent)
        {
            await _userEventRepository.AddAsync(userEvent);
        }

        public async Task<int> GetDailyVisitsAsync(DateTime date)
        {
            return await _userEventRepository.GetDailyVisitCountAsync(date);
        }

        public async Task<int> GetTotalVisitsAsync()
        {
            return await _userEventRepository.GetTotalVisitCountAsync();
        }
    }
}
