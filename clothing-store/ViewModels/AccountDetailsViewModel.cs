using clothing_store.Models;

namespace clothing_store.ViewModels
{
    public class AccountDetailsViewModel
    {
        public Account Account { get; set; }
        public List<OrderSummaryDto> Orders { get; set; }
    }

}
