using Project.TakuGames.Model.Dal;
using Proyect.TakuGames.Test.Helpers;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Moq;
using Xunit;
using System.Collections.Generic;
using System;

namespace Proyect.TakuGames.Test.Business
{
    [Collection("Mapper Collection")]

    public class OrderBusinessTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<OrderBusiness>> logger;
        private readonly IUnitOfWork unitOfWork;
        
        public OrderBusinessTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<OrderBusiness>>();
            unitOfWork = new FakeDAL.FakeUnitOfWork();
        }
        
        [Fact]
        public void  listarTodasLasOrdenesDeUnUsuarioTest()
        {        
            //arrange
            var customerOrder1 = new CustomerOrders() { OrderId = "1", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 1000 };
            var customerOrder2 = new CustomerOrders() { OrderId = "2", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 10000 };      
            var customerOrder3 = new CustomerOrders() { OrderId = "3", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 100000 };
            
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder1);
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder2);
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder3);

            var gameBusiness = new OrderBusiness(unitOfWork, mapper, logger.Object);
            
            //action
            var resp = gameBusiness.GetListCustumerOrderUser(1);

            //assert
            Assert.NotEmpty(resp);
            Assert.Equal(customerOrder1, resp[0]);
            Assert.Equal(customerOrder2, resp[1]);
            Assert.Equal(customerOrder3, resp[2]);
        }

        [Fact]
        public void ListarOrderDtoUserTest()
        {
            //arrange
            var customerOrder1 = new CustomerOrders() { OrderId = "1", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 3000 };
            var customerOrder2 = new CustomerOrders() { OrderId = "2", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 30000 };      
            var customerOrder3 = new CustomerOrders() { OrderId = "3", UserId=1, DateCreated = DateTime.Now.Date, CartTotal = 100000 };
            
            var customerOrderDetails1   = new CustomerOrderDetails(){ OrderDetailsId = 1, OrderId = "1", ProductId = 1, Quantity = 3, Price = 1000,  TotalPrice = 3000,  CoverFileName = "Image1"};
            var customerOrderDetails1_1 = new CustomerOrderDetails(){ OrderDetailsId = 2, OrderId = "1", ProductId = 10,Quantity = 2, Price = 1000,  TotalPrice = 3000,  CoverFileName = "Image1_1"};
            var customerOrderDetails2   = new CustomerOrderDetails(){ OrderDetailsId = 2, OrderId = "2", ProductId = 2, Quantity = 3, Price = 10000, TotalPrice = 30000, CoverFileName = "Image2"};
            var customerOrderDetails3   = new CustomerOrderDetails(){ OrderDetailsId = 3, OrderId = "3", ProductId = 3, Quantity = 1, Price = 100000,TotalPrice = 100000,CoverFileName = "Image3"};
            
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder1);        
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder2);        
            unitOfWork.CustomerOrdersRepository.Insert(customerOrder3);
            
            unitOfWork.CustomerOrderDetailsRepository.Insert(customerOrderDetails1);        
            unitOfWork.CustomerOrderDetailsRepository.Insert(customerOrderDetails2);
            unitOfWork.CustomerOrderDetailsRepository.Insert(customerOrderDetails3);
            unitOfWork.CustomerOrderDetailsRepository.Insert(customerOrderDetails1_1);
            
            var gameBusiness = new OrderBusiness(unitOfWork, mapper, logger.Object);       
        
            //action
            var resp = gameBusiness.GetOrdenUserDto(1);

            //assert
            Assert.NotEmpty(resp);
            Assert.IsType<List<OrdersUserDto>>(resp);
            Assert.Equal(customerOrderDetails1,   resp[0].CustomerOrderDetails[0]);
            Assert.Equal(customerOrderDetails1_1, resp[0].CustomerOrderDetails[1]);
            Assert.Equal(customerOrderDetails2,   resp[1].CustomerOrderDetails[0]);
            Assert.Equal(customerOrderDetails3,   resp[2].CustomerOrderDetails[0]);
        }
    }
}