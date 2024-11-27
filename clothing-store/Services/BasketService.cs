namespace clothing_store.Services
{
    using global::clothing_store.Interfaces;
    using global::clothing_store.Interfaces.clothing_store.Interfaces;
    using global::clothing_store.Models;
    using global::clothing_store.Repositories.clothing_store.Repositories;
    using System.Threading.Tasks;

    namespace clothing_store.Services
    {
        public class BasketService : IBasketService
        {
            private readonly IBasketRepository _basketRepository;
            private readonly IAccountRepository _accountRepository;
            private readonly IProductRepository _productRepository;
            public BasketService(IBasketRepository basketRepository,IAccountRepository accountRepository, IProductRepository productRepository)
            {
                _basketRepository = basketRepository;
                _accountRepository = accountRepository;
                _productRepository = productRepository;
            }

            public async Task<Basket> GetBasketByAccountIdAsync(int accountId)
            {
                return await _basketRepository.GetBasketByAccountIdAsync(accountId);
            }

            public async Task AddBasketAsync(Basket basket)
            {
                await _basketRepository.AddBasketAsync(basket);
            }

            public async Task UpdateBasketAsync(Basket basket)
            {
                await _basketRepository.UpdateBasketAsync(basket);
            }

            public async Task DeleteBasketByIdAsync(int basketId)
            {
                await _basketRepository.DeleteBasketByIdAsync(basketId);
            }
            public async Task DeleteBasketAsync(Basket basket) => await _basketRepository.DeleteBasketAsync(basket);
            public async Task AddProductToCartAsync(int accountId, Product product)
            {
                // Pobierz użytkownika z bazy danych
                var user = await _accountRepository.GetAccountByIdAsync(accountId);
                if (user == null)
                {
                    throw new ArgumentException("User do not exist.");
                }
                var basket = await _basketRepository.GetBasketByAccountIdAsync(user.AccountId);
                // Pobierz produkt
                if (product == null)
                {
                    throw new ArgumentException("Product do not exist");
                }

                // Dodaj produkt do koszyka użytkownika
                await _basketRepository.AddProductToCartAsync(basket, product);
            }
            public async Task<BasketProduct> GetBasketProductByIdAsync(int accountId, int productId)
            {
                return await _basketRepository.GetBasketProductByIdAsync(accountId, productId);
            }

            public async Task UpdateProductQuantityAsync(int accountId, int productId, int newQuantity)
            {
                var item = await _basketRepository.GetBasketProductByIdAsync(accountId, productId);

                if (item != null)
                {
                    item.Quantity = newQuantity;
                    await _basketRepository.UpdateBasketProductAsync(item);
                }
            }
            public async Task RemoveProductFromBasketAsync(int accountId, int productId)
            {
                var item = await _basketRepository.GetBasketProductByIdAsync(accountId, productId);

                if (item != null)
                {
                    await _basketRepository.RemoveBasketProductAsync(item);
                }
            }

        }
    }

}
