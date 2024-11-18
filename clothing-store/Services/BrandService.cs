using System.Runtime.CompilerServices;
using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> GetBrandByIdAsync(int Id)
        {
            return await _brandRepository.GetBrandByIdAsync(Id);
        }
        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }
    }
}
