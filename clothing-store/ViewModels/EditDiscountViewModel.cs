namespace clothing_store.ViewModels
{
    public class EditDiscountViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
    }

}
