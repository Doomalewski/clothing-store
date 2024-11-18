using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface ITaxService
    {
        public Task<List<Tax>> GetAllTaxesAsync();
        public Task AddTaxAsync(Tax tax);
        public Task<Tax> GetTaxByIdAsync(int Id);
    }
}
