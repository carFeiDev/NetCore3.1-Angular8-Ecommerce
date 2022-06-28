using Project.TakuGames.Dal;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Proyect.TakuGames.Test.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using Moq;
using AutoMapper;

namespace Proyect.TakuGames.Test.Business
{
    [Collection("Mapper Collection")]
    public class CartBusinessTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<CartBusiness>> logger;
        private readonly IUnitOfWork unitOfWork;
        public CartBusinessTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<CartBusiness>>();
            unitOfWork = new FakeDAL.FakeUnitOfWork();
        }

        [Fact]
        public void AgregaUnGameAlCarritoTest()
        {
            //arrange
            var game = new Game() { GameId = 1, Title = "title", };      
            var cart = new Cart() { CartId = "1", UserId = 1, DateCreated = DateTime.UtcNow, };
            var user = new UserMaster() { UserId = 1, UserName = "username1" };

            unitOfWork.GameRepository.Insert(game);
            unitOfWork.CartRepository.Insert(cart);
            unitOfWork.UserMasterRepository.Insert(user);
            
            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);
            
            //action
            cartbusiness.AddGameToCart(user.UserId, game.GameId);
            var resp = unitOfWork.CartItemsRepository.Get();

            //assert
            Assert.NotNull(resp);

        }

        [Fact]
        public void CreacionDelCarritoDeComprasTest()
        {
            //arange
            var user = new UserMaster { UserId = 1, UserName = "username" };
            unitOfWork.UserMasterRepository.Insert(user);

            var cartBusiness = new CartBusiness(unitOfWork, mapper, logger.Object);
            //action
            var resp = cartBusiness.CreateCart(user.UserId);

            //asser
            Assert.NotNull(resp);
            Assert.IsType<string>(resp);

        }

        [Fact]
        public void LimpiarCarritoTest()
        {
            //arrange
            var user = new UserMaster { UserId = 1, UserName = "username1" };
            unitOfWork.UserMasterRepository.Insert(user);

            Cart cart = new Cart { CartId = "1", DateCreated = DateTime.UtcNow, UserId = 1 };
            CartItems cartItems1 = new CartItems { CartItemId = 1, CartId = "1", ProductId = 3, Quantity = 2, };
            CartItems cartItems2 = new CartItems { CartItemId = 2, CartId = "1", ProductId = 4, Quantity = 3, };
            var game1 = new Game()
            {
                GameId = 3,
                Title = "title3",
                Description = "description3",
                Developer = "developer3",
                Publisher = "publisher3",
                Platform = "platform3",
                Category = "category3",
                Price = 3000,
                CoverFileName = "coverfilname3"
            };
            var game2 = new Game()
            {
                GameId = 4,
                Title = "title4",
                Description = "description4",
                Developer = "developer4",
                Publisher = "publisher4",
                Platform = "platform4",
                Category = "category4",
                Price = 5000,
                CoverFileName = "coverfilname4"
            };

            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.GameRepository.Insert(game2);
            unitOfWork.CartItemsRepository.Insert(cartItems1);
            unitOfWork.CartItemsRepository.Insert(cartItems2);
            unitOfWork.CartRepository.Insert(cart);

            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);
          
            //action
            var resp = cartbusiness.CleanCart(10);
            var cartItemCount = cartbusiness.GetCartItemCount(1);

            //assert
            Assert.Equal(0, resp);
            Assert.Equal(0, cartItemCount);
        }

        [Fact]
        public void EliminarCarritoPorIdTest()
        {
            //arrange
            Cart cart = new Cart { CartId = "1", DateCreated = DateTime.UtcNow, UserId = 1 };
            Cart cart2 = new Cart { CartId = "2", DateCreated = DateTime.UtcNow, UserId = 1 };

            unitOfWork.CartRepository.Insert(cart);
            unitOfWork.CartRepository.Insert(cart2);

            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);
            
            //action
            cartbusiness.DeleteTempCart("1");
            var resp = unitOfWork.CartRepository.Get().Where(x => x.CartId == "1");

            //assert
            Assert.Empty(resp);

        }

        [Fact]
        public void RestaEnUnoLaCantidadDeunGameEnElCarritoTest()
        {
            //assert
            var user = new UserMaster { UserId = 1, UserName = "username1" };
            Cart cart = new Cart { CartId = "1", DateCreated = DateTime.UtcNow, UserId = 1 };
            CartItems cartItems1 = new CartItems { CartItemId = 1, CartId = "1", ProductId = 3, Quantity = 10, };
           
            var game1 = new Game()
            {
                GameId = 3,
                Title = "title3",
                Description = "description3",
                Developer = "developer3",
                Publisher = "publisher3",
                Platform = "platform3",
                Category = "category3",
                Price = 3000,
                CoverFileName = "coverfilname3"
            };

            unitOfWork.UserMasterRepository.Insert(user);
            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.CartItemsRepository.Insert(cartItems1);
            unitOfWork.CartRepository.Insert(cart);

            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);

            //action
            cartbusiness.DeleteOneCardItem(user.UserId, game1.GameId);

            //assert
            Assert.Equal(9, cartItems1.Quantity);

        }
        
        [Fact]
        public void RemoverUnGameDelCarritoTest()
        {
            //arrange
            var user = new UserMaster { UserId = 1, UserName = "username1" };
            unitOfWork.UserMasterRepository.Insert(user);

            Cart cart = new Cart { CartId = "1", DateCreated = DateTime.UtcNow, UserId = 1 };
            CartItems cartItems1 = new CartItems { CartItemId = 1, CartId = "1", ProductId = 3, Quantity = 2, };
            CartItems cartItems2 = new CartItems { CartItemId = 2, CartId = "1", ProductId = 4, Quantity = 3, };
            var game1 = new Game()
            {
                GameId = 3,
                Title = "title3",
                Description = "description3",
                Developer = "developer3",
                Publisher = "publisher3",
                Platform = "platform3",
                Category = "category3",
                Price = 3000,
                CoverFileName = "coverfilname3"
            };
            var game2 = new Game()
            {
                GameId = 4,
                Title = "title4",
                Description = "description4",
                Developer = "developer4",
                Publisher = "publisher4",
                Platform = "platform4",
                Category = "category4",
                Price = 5000,
                CoverFileName = "coverfilname4"
            };

            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.GameRepository.Insert(game2);
            unitOfWork.CartItemsRepository.Insert(cartItems1);
            unitOfWork.CartItemsRepository.Insert(cartItems2);
            unitOfWork.CartRepository.Insert(cart);

            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);

            //action
            cartbusiness.RemoveCartItem(user.UserId, game1.GameId);
            var resp = unitOfWork.CartItemsRepository.Get().Where(x => x.CartItemId == 1);
            
            //assert
            Assert.Empty(resp);

        }

        [Fact]
        public void ObtenerLaSumaDeTodoslosElementosDeUnCArritoTest()
        {
            //arrange
            var game1 = new Game()
            {
                GameId = 3,
                Title = "title3",
                Description = "description3",
                Developer = "developer3",
                Publisher = "publisher3",
                Platform = "platform3",
                Category = "category3",
                Price = 3000,
                CoverFileName = "coverfilname3"
            };
            
            var user = new UserMaster { UserId = 1, UserName = "username1" };
            Cart cart = new Cart { CartId = "1", DateCreated = DateTime.UtcNow, UserId = 1 };
            CartItems cartItems1 = new CartItems { CartItemId = 1, CartId = "1", ProductId = 3, Quantity = 12, };
            
            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.CartItemsRepository.Insert(cartItems1);
            unitOfWork.CartRepository.Insert(cart);
            unitOfWork.UserMasterRepository.Insert(user);
            
            //action
            var cartbusiness = new CartBusiness(unitOfWork, mapper, logger.Object);
            var resp = cartbusiness.GetCartItemCount(user.UserId);

            //assert
            Assert.Equal(12, resp);
        }
    }
}
