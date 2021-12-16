using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;



namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]
    public class FavoriteControllerTests
    {
        private readonly Mock<IFavoritelistBusiness> mockFavoriteBusiness;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<FavoritelistController>> logger;     
        public FavoriteControllerTests(MapperFixture mapperFixture)
        {
            
             mapper = mapperFixture.mapper;
             logger = new Mock<ILogger<FavoritelistController>>();
             mockFavoriteBusiness = new Mock<IFavoritelistBusiness>();
             
        }

         [Fact]
         public async Task GetAllFavoriteUserTest(){
           
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
           
            List<Game> list = new List<Game>()
            {
                new Game(){GameId = 1,Title= "tittle1"},
                new Game(){GameId = 2,Title= "tittle1"},
            };

            mockFavoriteBusiness.Setup(b => b.GetUserFavoritelist(user1.UserId)).Returns(list);
            var ctl = new FavoritelistController( mockFavoriteBusiness.Object, mapper, logger.Object);

            //action
            var result = await ctl.Get(user1.UserId);

        
            // Assert
            var actionResult = Assert.IsType<ActionResult<List<GameVM>>>(result);
            var returnValue = Assert.IsType<List<GameVM>>(actionResult.Value);
            Assert.NotEmpty(returnValue);
           
         }

        [Fact]
         public async Task AddItemFavoriteUserTest(){
           
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            var game1 = new Game(){GameId = 3,Title= "tittle1"};
            
            List<Game> list = new List<Game>()
            {
                new Game(){GameId = 1,Title= "tittle1"},
                new Game(){GameId = 2,Title= "tittle1"},
            };

            mockFavoriteBusiness.Setup(b => b.ToggleFavoritelistItem(user1.UserId, game1.GameId));
            mockFavoriteBusiness.Setup(b => b.GetUserFavoritelist(user1.UserId)).Returns(list);
            var ctl = new FavoritelistController( mockFavoriteBusiness.Object, mapper, logger.Object);

            //action
            var result = await ctl.Post(user1.UserId, game1.GameId);

        
            // Assert
            var actionResult = Assert.IsType<ActionResult<List<GameVM>>>(result);
            var returnValue = Assert.IsType<List<GameVM>>(actionResult.Value);
            Assert.NotEmpty(returnValue);
           
         }

        [Fact]
         public void DeleteItemFavoriteUserTest(){
           
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            
            mockFavoriteBusiness.Setup(b => b.ClearFavoritelist(user1.UserId)).Returns(0);
            var ctl = new FavoritelistController( mockFavoriteBusiness.Object, mapper, logger.Object);

            //action
            var result =  ctl.Delete(user1.UserId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<int>>(result);
            var returnValue = Assert.IsType<int>(actionResult.Value);
            Assert.Equal(0,returnValue);
           
         }

    }
}