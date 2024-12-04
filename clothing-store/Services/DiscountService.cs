using clothing_store.Interfaces;

namespace clothing_store.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task CreateSpecialDiscountAsync(SpecialDiscount discount)
        {
            if (discount != null)
            {
                await _discountRepository.CreateSpecialDiscountAsync(discount);
            }
        }
    }
}
