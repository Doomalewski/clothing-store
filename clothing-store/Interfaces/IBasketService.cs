namespace clothing_store.Interfaces
{
    using global::clothing_store.Models;
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
            Task AddProductToCartAsync(int AccountId, Product ProductId);
            Task<BasketProduct> GetBasketProductByIdAsync(int accountId, int productId);
            Task UpdateProductQuantityAsync(int accountId, int productId, int newQuantity);
            Task RemoveProductFromBasketAsync(int accountId, int productId);
            Task<Basket> GetBasketByIdAsync(int basketId);

        }
    }

}
