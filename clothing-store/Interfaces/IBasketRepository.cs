﻿namespace clothing_store.Interfaces
{
    using global::clothing_store.Models;
    using System.Threading.Tasks;

    namespace clothing_store.Interfaces
    {
        public interface IBasketRepository
        {
            Task<Basket> GetBasketByAccountIdAsync(int accountId);
            Task AddBasketAsync(Basket basket);
            Task UpdateBasketAsync(Basket basket);
            Task DeleteBasketByIdAsync(int basketId);
            Task DeleteBasketAsync(Basket basket);
            Task AddProductToCartAsync(Basket basket, Product product);
            Task<BasketProduct> GetBasketProductByIdAsync(int accountId, int productId);
            Task UpdateBasketProductAsync(BasketProduct basketProduct);
            Task RemoveBasketProductAsync(BasketProduct basketProduct);
            Task ClearBasketByAccountIdAsync(int accountId);
            Task<Basket> GetBasketByIdAsync(int basketId);

        }
    }

}
