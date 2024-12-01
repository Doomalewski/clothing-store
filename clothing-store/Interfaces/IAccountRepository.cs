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

    }
}
