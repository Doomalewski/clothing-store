using clothing_store.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace clothing_store.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<string> _passwordHasher;
        public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _passwordHasher = new PasswordHasher<string>();
            _configuration = configuration;
        }
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _accountRepository.GetAccountByIdAsync(accountId);
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }
        public async Task AddAccountAsync(Account account) => await _accountRepository.AddAccountAsync(account);
        public async Task<bool> IsEmailInUse(string email)
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return accounts.Any(x => x.Email == email);
        }
        public string SaltAndHashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, string.Concat(password, _configuration.GetSection("Salt").Value));
        }
        public bool VerifyPassword(string hashedPassword, string password)
        {
            string saltedPassword = string.Concat(password, _configuration.GetSection("Salt").Value);
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, saltedPassword);
            return result == PasswordVerificationResult.Success;
        }
        public async Task DeleteAccountByIdAsync(int id) => await _accountRepository.DeleteAccountByIdAsync(id);
        public async Task DeleteAccountAsync(Account account) => await _accountRepository.DeleteAccountAsync(account);
        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await _accountRepository.GetAccountByEmailAsync(email);
        }
    }
}
