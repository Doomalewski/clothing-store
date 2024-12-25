namespace clothing_store.Repositories
{
    using global::clothing_store.Interfaces;
    using global::clothing_store.Models;
    using Microsoft.EntityFrameworkCore;

    namespace clothing_store.Repositories
    {
        public class UserEventRepository : IUserEventRepository
        {
            private readonly StoreDbContext _context;

            public UserEventRepository(StoreDbContext context)
            {
                _context = context;
            }

            public async Task AddAsync(UserEvent userEvent)
            {
                await _context.UserEvents.AddAsync(userEvent);
                await _context.SaveChangesAsync();
            }

            public async Task<int> GetDailyVisitCountAsync(DateTime date)
            {
                return await _context.UserEvents
                    .CountAsync(e => e.EventType == "Visit" && e.EventTime.Date == date.Date);
            }

            public async Task<int> GetTotalVisitCountAsync()
            {
                return await _context.UserEvents.CountAsync(e => e.EventType == "Visit");
            }
        }
    }

}
