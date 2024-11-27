using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class AddressRepository :IAddressRepository
    {
        private readonly StoreDbContext _context;
        public AddressRepository(StoreDbContext context) {
            _context = context;
        }
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == addressId);
        }
    }
}
