namespace clothing_store.Interfaces
{
    using System.Threading.Tasks;

    namespace clothing_store.Interfaces
    {
        public interface IBasketService
        {
            Task<Basket> GetBasketByAccountIdAsync(int accountId);
            Task AddBasketAsync(Basket basket);
            Task UpdateBasketAsync(Basket basket);
            Task DeleteBasketByIdAsync(int basketId);
            Task DeleteBasketAsync(Basket basket);
        }
    }

}
