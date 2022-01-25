using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;

namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]    
    public class CheckOutControllerTests
    {
        private readonly Mock<IOrderBusiness> mockOrderBusiness;
        private readonly Mock<ICartBusiness> mockCartBusiness;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<CheckOutController>> logger;

        public CheckOutControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<CheckOutController>>();
            mockOrderBusiness = new Mock<IOrderBusiness>();
            mockCartBusiness =  new Mock<ICartBusiness>();
        }

        [Fact]
        public void CreacionDeUnaOrdenTest()
        {
            //arrange
            var game1 = new Game() {GameId = 1,Title="title1"};
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            
            List<CustomerOrderDetails> listOrderDetails = new List<CustomerOrderDetails>()
            {
                new CustomerOrderDetails{OrderDetailsId = 1},
                new CustomerOrderDetails{OrderDetailsId = 2}
            };

            var cart = new Cart() { CartId= "1",UserId= 1,DateCreated= DateTime.Now.Date};           
            var orderdto= new OrdersDto() { OrderId= "1"};
            var cartItems = new CartItems() { CartId = "1",ProductId = game1.GameId, Quantity = 2  };


            mockOrderBusiness.Setup(b => b.CreateOrder(user1.UserId,orderdto)).Verifiable();
            mockCartBusiness.Setup(b => b.CleanCart(user1.UserId)).Verifiable();   
            var ctl = new CheckOutController( mockOrderBusiness.Object, mockCartBusiness.Object, mapper, logger.Object);

            //action
            var result =  ctl.Post(user1.UserId,orderdto);
    
            //assert
            mockOrderBusiness.Verify();
            mockCartBusiness.Verify();
          
        }        
    }
}