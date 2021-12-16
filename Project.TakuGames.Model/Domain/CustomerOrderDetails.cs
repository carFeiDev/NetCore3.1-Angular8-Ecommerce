
namespace Project.TakuGames.Model.Domain
{
    public partial class CustomerOrderDetails
    {
        public int OrderDetailsId { get; set; }
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string CoverFileName { get; set; }
    }
}
