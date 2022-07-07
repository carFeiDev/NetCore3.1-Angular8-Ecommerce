using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Project.TakuGames.Model.Dal;
using Proyect.TakuGames.Test.Helpers;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Domain;
using AutoMapper;
using Moq;
using Xunit;

namespace Proyect.TakuGames.Test.Business
{
    [Collection("Mapper Collection")]
    public class GameBusinessTests
    {
        private readonly IMapper mapper;     
        private readonly Mock<ILogger<GameBusiness>> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;
        public GameBusinessTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            memoryCache = serviceProvider.GetService<IMemoryCache>();
            logger = new Mock<ILogger<GameBusiness>>();
            unitOfWork = new FakeDAL.FakeUnitOfWork();
        }

        [Fact]
        public void ListarGamesTest()
        {
            //arrange
            var game1 = new Game()
            {   GameId = 1,
                Title = "Title1",
                Description = "description1",
                Developer = "developer1",
                Publisher = "publisher1",
                Platform = "platform1",
                Category = "category1",
                Price = 1000,
                CoverFileName = "coverfilname1"
            };

            var game2 = new Game()
            {   GameId = 2,
                Title = "Title2",
                Description = "description2",
                Developer = "developer2",
                Publisher = "publisher2",
                Platform = "platform2",
                Category = "category2",
                Price = 2000,
                CoverFileName = "coverfilname2"
             };

            var game3 = new Game() 
            {   GameId = 3,
                Title = "Title3",
                Description = "description3",
                Developer = "developer3",
                Publisher = "publisher3",
                Platform = "platform3",
                Category = "category3",
                Price = 3000,
                CoverFileName = "coverfilname3"
             };

            var game4 = new Game()
            {   GameId = 4,
                Title = "Title4",
                Description = "description4",
                Developer = "developer4",
                Publisher = "publisher4",
                Platform = "platform4",
                Category = "category4",
                Price = 4000,
                CoverFileName = "coverfilname4"
             };

            var game5 = new Game()
            {   GameId = 5,
                Title = "Title5",
                Description = "description5",
                Developer = "developer5",
                Publisher = "publisher5",
                Platform = "platform5",
                Category = "category5",
                Price = 5000,
                CoverFileName = "coverfilname5"
             };
            
            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.GameRepository.Insert(game2);
            unitOfWork.GameRepository.Insert(game3);
            unitOfWork.GameRepository.Insert(game4);
            unitOfWork.GameRepository.Insert(game5);

            var gameBusiness = new GameBusiness(unitOfWork, mapper,memoryCache, logger.Object);
            
            //action
            var resp = gameBusiness.GetAllGames();
            
            //assert
            Assert.NotEmpty(resp);
            Assert.IsType<List<Game>>(resp);
            Assert.Equal(game1,resp[0]);
            Assert.Equal(game2,resp[1]);
            Assert.Equal(game3,resp[2]);
            Assert.Equal(game4,resp[3]);
            Assert.Equal(game5,resp[4]); 
        }

        [Fact]
        public void ObtenerGamePorIdTest()
        {
            //arrange
            var idgame = 2;
            var game1 = new Game() { GameId = 1, Title = "Title1" };
            var game2 = new Game() { GameId = 2, Title = "Title2" };
            
            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.GameRepository.Insert(game2);

            var gameBusiness = new GameBusiness(unitOfWork, mapper,memoryCache, logger.Object);
            
            //action
            var resp = gameBusiness.GetGame(idgame);

            //assert
            Assert.NotNull(resp);
            Assert.Equal(idgame, resp.GameId);

        }

        [Fact]
        public void CrearGameTest()
        {
            //arrange
            var game = new Game()
            {
                GameId = 1,
                Title = "title",
                Description = "description",
                Developer = "developer",
                Publisher = "publisher",
                Platform = "platform",
                Category = "category",
                Price = 3000,
                CoverFileName = "coverfilname"
            };

            var gameBusiness = new GameBusiness(unitOfWork, mapper,memoryCache, logger.Object);            
            
            //action
            var resp = gameBusiness.CreateGame(game);
            
            //assert
            Assert.NotNull(resp);
            Assert.Equal(1, resp.GameId);
            Assert.Equal("title", resp.Title);
            Assert.Equal("description", resp.Description);
            Assert.Equal("developer", resp.Developer);
            Assert.Equal("publisher", resp.Publisher);
            Assert.Equal("platform", resp.Platform);
            Assert.Equal("category", resp.Category);
            Assert.Equal(3000, resp.Price);
            Assert.Equal("coverfilname", resp.CoverFileName);
        }

        [Fact]
        public void ModificarTodosLosElementosGameTest()
        {
            //arrange
            var game = new Game()
            {
                GameId = 1,
                Title = "title",
                Description = "description",
                Developer = "developer",
                Publisher = "publisher",
                Platform = "platform",
                Category = "category",
                Price = 3000,
                CoverFileName = "coverfilname"
            };
            var newgame = new Game()
            {
                GameId = 1,
                Title = "titlenew",
                Description = "descriptionnew",
                Developer = "developernew",
                Publisher = "publishernew",
                Platform = "platformnew",
                Category = "categorynew",
                Price = 5000,
                CoverFileName = "coverfilnamenew"
            };

            unitOfWork.GameRepository.Insert(game);
            var gamebusiness = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);

            //action
            var resp = gamebusiness.UpdateGame(newgame);

            //assert
            Assert.NotNull(resp);
            Assert.Equal("titlenew", resp.Title);
            Assert.Equal("descriptionnew", resp.Description);
            Assert.Equal("developernew", resp.Developer);
            Assert.Equal("publishernew", resp.Publisher);
            Assert.Equal("platformnew", resp.Platform);
            Assert.Equal("categorynew", resp.Category);
            Assert.Equal(5000, resp.Price);
            Assert.Equal("coverfilnamenew", resp.CoverFileName);
            Assert.Equal(1, resp.GameId);
        }

        [Fact]
        public void ModificarAlgunosElementosGameTest()
        {
            //arrange
            var game = new Game()
            {
                GameId = 1,
                Title = "title",
                Description = "description",
                Developer = "developer",
                Publisher = "publisher",
                Platform = "platform",
                Category = "category",
                Price = 3000,
                CoverFileName = "coverfilname"
            };
            var newgame = new Game()
            {
                GameId = 1,
                Title = "titlenew",
                Description = "descriptionnew",
                Developer = "developernew",
                Publisher = "publisher",
                Platform = "platform",
                Category = "category",
                Price = 3000,
                CoverFileName = "coverfilname"
            };
            
            unitOfWork.GameRepository.Insert(game);
            var gamebusiness = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);


            //action
            var resp = gamebusiness.UpdateGame(newgame);

            //assert
            Assert.NotNull(resp);
            Assert.Equal("titlenew", resp.Title);
            Assert.Equal("descriptionnew", resp.Description);
            Assert.Equal("developernew", resp.Developer);
            Assert.Equal("publisher", resp.Publisher);
            Assert.Equal("platform", resp.Platform);
            Assert.Equal("category", resp.Category);
            Assert.Equal(3000, resp.Price);
            Assert.Equal("coverfilname", resp.CoverFileName);
            Assert.Equal(1, resp.GameId);
        }

        [Fact]
        public void EliminarGameTest()
        {
            //arrange
            var gameId = 1;
            var game = new Game()
            {
                GameId = 1,
                Title = "title",
                Description = "description",
                Developer = "developer",
                Publisher = "publisher",
                Platform = "platform",
                Category = "category",
                Price = 3000,
                CoverFileName = "coverfilname"
            };

            unitOfWork.GameRepository.Insert(game);           
            var bis = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);

            //action
            var returnImage = bis.DeleteGame(gameId);
            var deletegame = bis.GetGame(gameId);

            //assert
            Assert.Equal("coverfilname", returnImage);
            Assert.Null(deletegame);

            
        }

        [Fact]
        public void ObtenerTodasLasCategoriasTest()
        {
            //arrange
            var categorie1 = new Categories { CategoryId = 1, CategoryName = "categoryname1",};
            var categorie2 = new Categories { CategoryId = 2, CategoryName = "categoryname2",};
            
            unitOfWork.CategoriesRepository.Insert(categorie1);
            unitOfWork.CategoriesRepository.Insert(categorie2);
            
            var gameBusiness = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);
            
            //action
            var listCategories = gameBusiness.GetCategories();
            
            //assert
            Assert.NotEmpty(listCategories);
            Assert.Equal(1, listCategories[0].CategoryId); 
            Assert.Equal("categoryname1", listCategories[0].CategoryName);
            Assert.Equal(2, listCategories[1].CategoryId);
            Assert.Equal("categoryname2", listCategories[1].CategoryName);
        }

        [Fact]
        public void ObtenerItemsDelCartTest()
        {
            //arrange
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

            var gameBusiness = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);
            
            //action
            var listCart = gameBusiness.GetGameAvailableInCart("1");
            
            //Assert
            Assert.NotEmpty(listCart);
            Assert.Equal(game1, listCart[0].Game);
            Assert.Equal(cartItems1.Quantity, listCart[0].Quantity);
            Assert.Equal(game2, listCart[1].Game);
            Assert.Equal(cartItems2.Quantity, listCart[1].Quantity);
        }
        
        [Fact]
        public void ObtenerGamesSimilaresTest()
        {
            //arrange
            var game1 = new Game() { GameId = 1, Title = "Title1", Category="Carreras" };
            var game2 = new Game() { GameId = 2, Title = "Title2",Category="Carreras" };
            var game3 = new Game() { GameId = 3, Title = "Title3", Category ="Carreras" };
            
            unitOfWork.GameRepository.Insert(game1);
            unitOfWork.GameRepository.Insert(game2);
            unitOfWork.GameRepository.Insert(game3);

            var gameBusiness = new GameBusiness(unitOfWork, mapper, memoryCache, logger.Object);
            
            //action
            var resp = gameBusiness.GetSimilarGames(game1.GameId);
            
            //assert
            Assert.NotEmpty(resp);
            Assert.NotEqual(game1.GameId, resp[1].GameId);
            Assert.Equal(game2.Category, resp[1].Category);
            Assert.NotEqual(game1.GameId, resp[0].GameId);
            Assert.Equal(game3.Category, resp[0].Category);
        }
    }
}




