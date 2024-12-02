using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IPDFService
    {
        public byte[] GenerateProductPriceListPdf(List<Product> products);
    }
}
