namespace clothing_store.Repositories
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
        }
    }

}
