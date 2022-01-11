
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using System;


namespace Project.TakuGames.Dal
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DataBaseContext context;
        public UnitOfWork(DataBaseContext dbContext)
        {
            context = dbContext;
        }

        private IGenericRepository<Game> gameRepository;
        public IGenericRepository<Game> GameRepository
        {
            get
            {

                if (this.gameRepository == null)
                {
                    this.gameRepository = new GenericRepository<Game>(context);
                }
                return gameRepository;
            }
        }

        private IGenericRepository<Categories> categoriesRepository;
        public  IGenericRepository<Categories> CategoriesRepository
        {
            get
            {
                if (this.categoriesRepository == null)
                {
                    this.categoriesRepository = new GenericRepository<Categories>(context);
                }
                return categoriesRepository;
            }
        }
        private IGenericRepository<CartItems> cartitemsRepository;
        public IGenericRepository<CartItems> CartItemsRepository
        {
            get
            {
                if (this.cartitemsRepository == null)
                {
                    this.cartitemsRepository = new GenericRepository<CartItems>(context);
                }
                return cartitemsRepository; 
            }
        }
        private IGenericRepository<Cart> cartRepository;
        public IGenericRepository<Cart> CartRepository
        {
            get
            {
                if (this.cartRepository == null)
                {
                    this.cartRepository = new GenericRepository<Cart>(context);
                }
                return cartRepository;
            }
        }
        private IGenericRepository<UserMaster> usermasterRepository;
        public IGenericRepository<UserMaster> UserMasterRepository
        {
            get
            {
                if (this.usermasterRepository == null)
                {
                    this.usermasterRepository = new GenericRepository<UserMaster>(context);
                }
                return usermasterRepository;
            }
        }
        private IGenericRepository<CustomerOrders> customerOrdersRepository;
        public IGenericRepository<CustomerOrders> CustomerOrdersRepository
        {
            get
            {
                if (this.customerOrdersRepository == null)
                {
                    this.customerOrdersRepository = new GenericRepository<CustomerOrders>(context);
                }
                return customerOrdersRepository;
            }
        }
        private IGenericRepository<CustomerOrderDetails> customerOrderDetailsRepository;
        public IGenericRepository<CustomerOrderDetails> CustomerOrderDetailsRepository
        {
            get
            {
                if (this.customerOrderDetailsRepository == null)
                {
                    this.customerOrderDetailsRepository = new GenericRepository<CustomerOrderDetails>(context);
                }
                return customerOrderDetailsRepository;
            }
        }
        private IGenericRepository<Favoritelist> favoritelistRepository;
        public IGenericRepository<Favoritelist> FavoritelistRepository
        {
            get
            {
                if (this.favoritelistRepository == null)
                {
                    this.favoritelistRepository = new GenericRepository<Favoritelist>(context);
                }
                return favoritelistRepository;
            }
        }


        private IGenericRepository<FavoritelistItems> favoritelistItemsRepository;
        public IGenericRepository<FavoritelistItems> FavoritelistItemsRepository
        {
            get
            {
                if (this.favoritelistItemsRepository == null)
                {
                    this.favoritelistItemsRepository = new GenericRepository<FavoritelistItems>(context);
                }
                return favoritelistItemsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
