using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IDiscountRepository
    {
        public Task CreateSpecialDiscountAsync(SpecialDiscount specialDiscount);

    }
}
