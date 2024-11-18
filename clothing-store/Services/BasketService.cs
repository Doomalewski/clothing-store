namespace clothing_store.Services
{
    using global::clothing_store.Interfaces.clothing_store.Interfaces;
    using global::clothing_store.Repositories.clothing_store.Repositories;
    using System.Threading.Tasks;

    namespace clothing_store.Services
    {
        public class BasketService : IBasketService
        {
            private readonly IBasketRepository _basketRepository;

            public BasketService(IBasketRepository basketRepository)
            {
                _basketRepository = basketRepository;
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
        }
    }

}
