namespace clothing_store.Interfaces
{
    public interface IDiscountService
    {
        public Task CreateSpecialDiscountAsync(SpecialDiscount specialDiscount);

    }
}
