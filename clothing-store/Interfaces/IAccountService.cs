using clothing_store.Repositories;
using System.Runtime.CompilerServices;

namespace clothing_store.Interfaces
{
    public interface IAccountService
    {
        public Task<Account> GetAccountByIdAsync(int accountId);
        public Task<List<Account>> GetAllAccountsAsync();
        public Task AddAccountAsync(Account account);
        public Task<bool> IsEmailInUse(string email);
        public string SaltAndHashPassword(string password);
        public Task DeleteAccountByIdAsync(int id);
        public Task DeleteAccountAsync(Account account);
        public Task<Account> GetAccountByEmailAsync(string email);
        public bool VerifyPassword(string hashedPassword, string password);
        public Task<Account> GetAccountFromHttpAsync();
        public Task ClearBasketAsync(int accountId);
        public Task<Address> GetAddressByIdAsync(int addressId);

    }   
}
