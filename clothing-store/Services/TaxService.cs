using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace clothing_store.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _TaxRepository;
        public TaxService(ITaxRepository taxRepository)
        {
            _TaxRepository = taxRepository;
        }

        public async Task<List<Tax>> GetAllTaxesAsync()
        {
            return await _TaxRepository.GetAllTaxesAsync();

        }
        public async Task AddTaxAsync(Tax tax) => await _TaxRepository.AddTaxAsync(tax);
        public async Task<Tax> GetTaxByIdAsync(int Id)
        {
            return await _TaxRepository.GetTaxByIdAsync(Id);
        }
    }
}
