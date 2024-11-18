using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IBrandService
    {
        public Task<Brand> GetBrandByIdAsync(int Id);
        public Task<List<Brand>> GetAllBrandsAsync();
    }
}
