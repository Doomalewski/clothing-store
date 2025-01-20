namespace clothing_store.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> GetAccountByIdAsync(int accountId);
        public Task<List<Account>> GetAllAccountsAsync();
        public Task AddAccountAsync(Account account);
        public Task DeleteAccountByIdAsync(int id);
        public Task DeleteAccountAsync(Account account);
        public Task<Account> GetAccountByEmailAsync(string email);
        public Task UpdateResetTokenAsync(int accountId, string token, DateTime expiration);
        public Task UpdatePasswordAndClearTokenAsync(int accountId, string hashedPassword);
        public Task UpdateAccountAsync(Account account);

    }
}
