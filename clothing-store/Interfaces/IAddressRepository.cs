using clothing_store.Repositories;

namespace clothing_store.Interfaces
{
    public interface IAddressRepository
    {
        public Task<Address> GetAddressByIdAsync(int addressId);
    }
}
