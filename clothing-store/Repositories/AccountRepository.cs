using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StoreDbContext _context;
        public async Task<Account> GetAccountAsync(int accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task AddAccountAsync(Account account) 
        {
            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
