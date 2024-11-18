using clothing_store.Interfaces;

namespace clothing_store.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Account> GetAccountAsync(int accountId)
        {
            return await _accountRepository.GetAccountAsync(accountId);
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }
        public async Task AddAccountAsync(Account account) => await _accountRepository.AddAccountAsync(account);
    }
}
