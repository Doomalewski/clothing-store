﻿using clothing_store.Interfaces;
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
        public AccountService(IAccountRepository accountRepository, IConfiguration configuration,IHttpContextAccessor httpContext,IBasketRepository basketRepository,IAddressRepository addressRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = new PasswordHasher<string>();
            _configuration = configuration;
            _httpContextAccessor = httpContext;
            _basketRepository = basketRepository;
            _addressRepository = addressRepository;
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
    }
}
