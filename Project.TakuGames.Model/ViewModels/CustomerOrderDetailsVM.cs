
namespace Project.TakuGames.Model.ViewModels
{
    public class CustomerOrderDetailsVM
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
