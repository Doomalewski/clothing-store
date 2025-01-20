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
            var account = await _context.Accounts
             .Include(a => a.Basket)
             .Include(a => a.Orders)
             .Include(a => a.Address)
             .Include(a => a.Discounts)
             .FirstOrDefaultAsync(a => a.AccountId == accountId);

            return account; ;
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
        public async Task<Account> GetAccountByEmailAsync(string email)
        {
           var account = await _context.Accounts
                 .Include(a => a.Basket)
                 .Include(a => a.Orders)
                 .Include(a => a.Address)
                 .Include(a => a.Discounts)
                 .FirstOrDefaultAsync(a => a.Email == email);
            return account;
        }
        public async Task UpdateResetTokenAsync(int accountId, string token, DateTime expiration)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account != null)
            {
                account.ResetToken = token;
                account.ResetTokenExpiration = expiration;
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdatePasswordAndClearTokenAsync(int accountId, string hashedPassword)
        {
            var account = await GetAccountByIdAsync(accountId);
            if (account != null)
            {
                account.Password = hashedPassword;
                account.ResetToken = "";
                account.ResetTokenExpiration = DateTime.UtcNow;

                _context.Accounts.Update(account); // Lub trackowanie encji, jeśli automatyczne
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
