using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using Project.TakuGames.Model.ViewModels;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;
using System.Collections.Generic;
using Xunit;

namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]
    public class ShoppingCartControllerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<ShoppingCartController>> logger;
        private readonly Mock<IGameBusiness> mockGameBusiness;
        private readonly Mock<ICartBusiness> mockCartBusiness;

        public ShoppingCartControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<ShoppingCartController>>();
            mockGameBusiness = new Mock<IGameBusiness>();
            mockCartBusiness = new Mock<ICartBusiness>();
        }

        [Fact]      
        public void ObtenerTodosLosItemDelCarritoTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            var car1 = new CartItems() { CartId = "1", ProductId = 1, Quantity = 2, CartItemId = 1 };
            var game1 = new Game() { GameId = 1, Title = "title1" };
            var game2 = new Game() { GameId = 2, Title = "tittle2" };

            List<CartItemDto> listDto = new List<CartItemDto>()
            {
                new CartItemDto(){Game=game1,Quantity=2},
                new CartItemDto(){Game=game2,Quantity=2},
            };

            mockCartBusiness.Setup(b => b.GetCartId(user1.UserId)).Returns(car1.CartId).Verifiable();
            mockGameBusiness.Setup(b => b.GetGameAvailableInCart(car1.CartId)).Returns(listDto).Verifiable();
    
            var ctl = new ShoppingCartController(mockCartBusiness.Object, mockGameBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Get(1);

            //assert
            var actionResult = Assert.IsType<ActionResult<List<CartItemDtoVM>>>(resp);
            var returnValue = Assert.IsType<List<CartItemDtoVM>>(actionResult.Value);
            mockGameBusiness.VerifyAll();
            mockCartBusiness.VerifyAll();
            Assert.NotEmpty(returnValue);
       
        }

        [Fact]
        public void AgregarUnItemAlCArritoTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            var car1  = new CartItems() { CartId = "1", ProductId = 1, Quantity = 2, CartItemId = 1 };
            var game1 = new Game() { GameId = 1, Title = "title1" };
            var game2 = new Game() { GameId = 2, Title = "title2" };

            mockCartBusiness.Setup(b => b.AddGameToCart(user1.UserId,game1.GameId));
            mockCartBusiness.Setup(b => b.GetCartItemCount(user1.UserId)).Returns(1);

            var ctl = new ShoppingCartController(mockCartBusiness.Object, mockGameBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Post(user1.UserId,game1.GameId);
            
            //assert
            var actionResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(actionResult.Value);       
            
            Assert.Equal(1,returnValue);

        }
        [Fact]
        public void EliminarUnItemDelCarritoTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };         
            var game1 = new Game() { GameId = 1, Title = "title1" };
            
            mockCartBusiness.Setup(b => b.DeleteOneCardItem(user1.UserId, game1.GameId));
            mockCartBusiness.Setup(b => b.GetCartItemCount(user1.UserId)).Returns(0);

            var ctl = new ShoppingCartController(mockCartBusiness.Object, mockGameBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Put(user1.UserId, game1.GameId);

            //assert
            var actionResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(actionResult.Value);
            
            Assert.Equal(0, returnValue);
        }

        [Fact]
        public void EliminarItemDelCarritoTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            var game1 = new Game() { GameId = 1, Title = "title1" };

            mockCartBusiness.Setup(b => b.RemoveCartItem(user1.UserId, game1.GameId));
            mockCartBusiness.Setup(b => b.GetCartItemCount(user1.UserId)).Returns(0);

            var ctl = new ShoppingCartController(mockCartBusiness.Object, mockGameBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Delete(user1.UserId, game1.GameId);

            //assert
            var actionResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(actionResult.Value);

            Assert.Equal(0, returnValue);
        }
        [Fact]
        public void LimpiarTodoslosItemsDelCarritoTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            var game1 = new Game() { GameId = 1, Title = "title1" };

            mockCartBusiness.Setup(b => b.CleanCart(user1.UserId)).Returns(0);

            var ctl = new ShoppingCartController(mockCartBusiness.Object, mockGameBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Delete(user1.UserId);

            //assert
            var actionResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(actionResult.Value);

            Assert.Equal(0, returnValue);
        }
    }
}
