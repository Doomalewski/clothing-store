namespace clothing_store.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> GetAccountAsync(int accountId);
        public Task<List<Account>> GetAllAccountsAsync();
        public Task AddAccountAsync(Account account);
    }
}
