
using Project.TakuGames.Model.Domain;

namespace Project.TakuGames.Model.Dal
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> GameRepository { get; }
        IGenericRepository<Categories> CategoriesRepository { get; }
        IGenericRepository<CartItems> CartItemsRepository { get; }
        IGenericRepository<Cart> CartRepository { get; }
        IGenericRepository<UserMaster> UserMasterRepository { get; }
        IGenericRepository<CustomerOrders> CustomerOrdersRepository { get; }
        IGenericRepository<CustomerOrderDetails> CustomerOrderDetailsRepository { get; }
        IGenericRepository<Favoritelist> FavoritelistRepository { get; }
        IGenericRepository<FavoritelistItems> FavoritelistItemsRepository { get; }
        
        void Dispose();
        void Save();
    }
}
