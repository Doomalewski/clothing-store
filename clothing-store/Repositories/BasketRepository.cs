﻿namespace clothing_store.Repositories
{
    using global::clothing_store.Interfaces.clothing_store.Interfaces;
    using global::clothing_store.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace clothing_store.Repositories
    {
        public class BasketRepository : IBasketRepository
        {
            private readonly StoreDbContext _context;

            public BasketRepository(StoreDbContext context)
            {
                _context = context;
            }

            public async Task<Basket> GetBasketByAccountIdAsync(int accountId)
            {
                var basket = await _context.Baskets
                    .Include(b => b.BasketProducts)
                        .ThenInclude(b=>b.Product)
                    .Include(b=>b.Account)
                    .FirstOrDefaultAsync(b => b.AccountId == accountId);

                return basket;
            }


            public async Task AddBasketAsync(Basket basket)
            {
                await _context.Baskets.AddAsync(basket);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateBasketAsync(Basket basket)
            {
                _context.Baskets.Update(basket);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteBasketByIdAsync(int basketId)
            {
                var basket = await _context.Baskets.FindAsync(basketId);
                if (basket != null)
                {
                    _context.Baskets.Remove(basket);
                    await _context.SaveChangesAsync();
                }
            }
            public async Task DeleteBasketAsync(Basket basket)
            {
                if(basket != null)
                {
                    _context.Baskets.Remove(basket);
                    await _context.SaveChangesAsync();
                }
            }
            public async Task AddProductToCartAsync(Basket basket, Product product)
            {
                // Sprawdź, czy koszyk istnieje
                if (basket == null)
                {
                    throw new ArgumentNullException(nameof(basket), "Koszyk nie może być null.");
                }

                // Sprawdź, czy produkt istnieje
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product), "Produkt nie może być null.");
                }

                // Sprawdź, czy produkt jest już w koszyku
                var existingBasketProduct = await _context.BasketProducts
                    .FirstOrDefaultAsync(bp => bp.BasketId == basket.BasketId && bp.ProductId == product.ProductId);

                if (existingBasketProduct != null)
                {
                    // Jeśli produkt już jest w koszyku, zwiększ ilość
                    existingBasketProduct.Quantity += 1;
                }
                else
                {
                    // Jeśli produkt nie jest w koszyku, dodaj nowy wpis
                    var productToAdd = new BasketProduct
                    {
                        ProductId = product.ProductId,
                        BasketId = basket.BasketId,
                        Quantity = 1
                    };
                    _context.BasketProducts.Add(productToAdd);
                }

                // Zapisz zmiany w bazie danych
                await _context.SaveChangesAsync();
            }
            public async Task<BasketProduct> GetBasketProductByIdAsync(int accountId, int productId)
            {
                return await _context.BasketProducts
                    .FirstOrDefaultAsync(b => b.Basket.AccountId == accountId && b.ProductId == productId);
            }

            public async Task UpdateBasketProductAsync(BasketProduct item)
            {
                _context.BasketProducts.Update(item);
                await _context.SaveChangesAsync();
            }
            public async Task RemoveBasketProductAsync(BasketProduct item)
            {
                _context.BasketProducts.Remove(item);
                await _context.SaveChangesAsync();
            }
            public async Task ClearBasketByAccountIdAsync(int accountId)
            {
                // Znajdź koszyk powiązany z podanym AccountId
                var basket = await _context.Baskets
                    .Include(b => b.BasketProducts)
                    .ThenInclude(bp => bp.Product)
                    .FirstOrDefaultAsync(b => b.AccountId == accountId);

                if (basket == null)
                {
                    throw new InvalidOperationException($"Koszyk dla użytkownika z ID {accountId} nie został znaleziony.");
                }

                // Usuń wszystkie produkty z koszyka
                if (basket.BasketProducts != null && basket.BasketProducts.Any())
                {
                    _context.BasketProducts.RemoveRange(basket.BasketProducts);
                }

                // Zapisz zmiany w bazie danych
                await _context.SaveChangesAsync();
            }
            public async Task<Basket> GetBasketByIdAsync(int basketId)
            {
                return await _context.Baskets
                            .Include(b => b.BasketProducts)
                            .ThenInclude(bp => bp.Product)
                            .FirstOrDefaultAsync(b=>b.BasketId == basketId);
            }

        }
    }

}
