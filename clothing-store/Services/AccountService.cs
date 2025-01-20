using clothing_store.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using clothing_store.Interfaces.clothing_store.Interfaces;

namespace clothing_store.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketRepository _basketRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        public AccountService(IAccountRepository accountRepository, IConfiguration configuration,IHttpContextAccessor httpContext,IBasketRepository basketRepository,IAddressRepository addressRepository, IOrderRepository orderRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = new PasswordHasher<string>();
            _configuration = configuration;
            _httpContextAccessor = httpContext;
            _basketRepository = basketRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
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

        public async Task<Account> GetAccountFromHttpAsync()
        {
            var accountIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

            if (accountIdClaim == null)
            {
                return null;
            }

            if (!int.TryParse(accountIdClaim.Value, out var accountId))
            {
                return null;
            }

            return await _accountRepository.GetAccountByIdAsync(accountId);
        }
        public async Task ClearBasketAsync(int accountId) => await _basketRepository.ClearBasketByAccountIdAsync(accountId);
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _addressRepository.GetAddressByIdAsync(addressId);
        }
        public async Task<List<Order>> GetOrdersByAccountIdAsync(int accountId)
        {
            return await _orderRepository.GetOrdersByAccountIdAsync(accountId);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(int accountId)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null) throw new ArgumentException("Account not found.");

            // Generowanie tokenu
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var expiration = DateTime.UtcNow.AddHours(1);

            await _accountRepository.UpdateResetTokenAsync(accountId, token, expiration);

            return token;
        }

        public async Task<bool> ResetPasswordAsync(int accountId, string token, string newPassword)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null || account.ResetToken != token || account.ResetTokenExpiration < DateTime.UtcNow)
            {
                return false; // Token nieważny
            }

            var hashedPassword = SaltAndHashPassword(newPassword);
            await _accountRepository.UpdatePasswordAndClearTokenAsync(accountId, hashedPassword);

            return true;
        }


        public async Task<bool> ValidateResetTokenAsync(int accountId, string token)
        {
            var account = await _accountRepository.GetAccountByIdAsync(accountId);
            if (account == null || account.ResetToken != token || account.ResetTokenExpiration < DateTime.UtcNow)
            {
                return false; // Token jest nieważny lub nie istnieje
            }

            return true;
        }
        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAccountAsync(account);
        }
    }
}
