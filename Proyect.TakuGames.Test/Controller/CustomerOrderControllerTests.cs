using System;
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
    public class CustomerOrderControllerTests
    {

        private readonly Mock<IOrderBusiness> mockOrderBusiness;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<CustomerOrderController>> logger;

         public CustomerOrderControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<CustomerOrderController>>();
            mockOrderBusiness = new Mock<IOrderBusiness>();
            
        }

        [Fact]
        public void ObtenerListaTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };

            List<CustomerOrderDetails> listOrderDetails = new List<CustomerOrderDetails>(){
                new CustomerOrderDetails{OrderDetailsId = 1},
                new CustomerOrderDetails{OrderDetailsId=2}
            };

            List<OrdersUserDto> list = new List<OrdersUserDto>()
            {
                new OrdersUserDto(){CustomerOrderDetails= listOrderDetails , DateCreated= DateTime.Now.Date},
                new OrdersUserDto(){CustomerOrderDetails= listOrderDetails, DateCreated= DateTime.Now.Date},
            };

            mockOrderBusiness.Setup(b => b.GetOrdenUserDto(user1.UserId)).Returns(list).Verifiable();;   
            var ctl = new CustomerOrderController( mockOrderBusiness.Object, mapper, logger.Object);

            //action
            var result =  ctl.Get(user1.UserId);

        
            //assert
            var actionResult = Assert.IsType<ActionResult<List<OrdersUserDtoVM>>>(result);
            var returnValue  = Assert.IsType<List<OrdersUserDtoVM>>(actionResult.Value);
            mockOrderBusiness.Verify();
            Assert.NotEmpty(returnValue);

        }
    
    }

}