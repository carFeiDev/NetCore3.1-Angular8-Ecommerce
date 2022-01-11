using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using System;

namespace Proyect.TakuGames.Test.FakeDAL
{
    public class FakeUnitOfWork : IDisposable, IUnitOfWork
    {
        public IGenericRepository<Game> GameRepository { get; set; }
        public IGenericRepository<Categories> CategoriesRepository { get; set; }
        public IGenericRepository<CartItems> CartItemsRepository { get; set; }
        public IGenericRepository<Cart> CartRepository { get; set; }
        public IGenericRepository<UserMaster> UserMasterRepository { get; set; }
        public IGenericRepository<CustomerOrders> CustomerOrdersRepository { get; set; }
        public IGenericRepository<CustomerOrderDetails> CustomerOrderDetailsRepository { get; set; }
        public IGenericRepository<Favoritelist> FavoritelistRepository { get; set; }
        public IGenericRepository<FavoritelistItems> FavoritelistItemsRepository { get; set; }



        public FakeUnitOfWork()
        {
            GameRepository = new FakeRepository<Game>();
            CategoriesRepository = new FakeRepository<Categories>();
            CartItemsRepository = new FakeRepository<CartItems>();
            CartRepository = new FakeRepository<Cart>();
            UserMasterRepository = new FakeRepository<UserMaster>();
            CustomerOrdersRepository = new FakeRepository<CustomerOrders>();
            CustomerOrderDetailsRepository = new FakeRepository<CustomerOrderDetails>();
            FavoritelistRepository = new FakeRepository<Favoritelist>();
            FavoritelistItemsRepository = new FakeRepository<FavoritelistItems>();

        }

        public void Dispose()
        {

        }

        public void Save()
        {

        }
    }
}
