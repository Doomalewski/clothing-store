using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StoreDbContext _context;
        public AccountRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
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
        public async Task DeleteAccountByIdAsync(int id)
        {
            var accountToDelete = _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
            _context.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAccountAsync(Account account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
        }

    }
}
