
namespace Project.TakuGames.Model.Business
{
    public interface ICartBusiness
    {
        void AddGameToCart(int userId, int gameId);
        void RemoveCartItem(int userId, int gameId);
        void DeleteOneCardItem(int userId, int gameId);
        int GetCartItemCount(int userId);
        void MergeCart(int tempUserId, int permUserId);
        int CleanCart(int userId);
        string GetCartId(int userId);

    }
}
