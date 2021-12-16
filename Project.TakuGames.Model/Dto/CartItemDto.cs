using Project.TakuGames.Model.Domain;

namespace Project.TakuGames.Model.Dto
{
    public class CartItemDto
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }

    }
}
