using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;

namespace Project.TakuGames.Business
{
    public class OrderBusiness: BaseBusiness, IOrderBusiness
    {
        public OrderBusiness(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             ILogger<OrderBusiness> logger)
                             : base(unitOfWork, mapper, logger)
                             {}

        public void CreateOrder(int userId, OrdersDto orderDetails)
        {
            try
            {
                //StringBuilder orderid = new StringBuilder();
                //orderid.Append(CreateRandomNumber(3));
                //orderid.Append('-');
                //orderid.Append(CreateRandomNumber(6));
                List<CustomerOrderDetails> customerOrderDetails = new List<CustomerOrderDetails>();                                     
                CustomerOrders custumerOrders = new CustomerOrders();
                custumerOrders.OrderId = Guid.NewGuid().ToString();
                custumerOrders.UserId = userId;
                custumerOrders.DateCreated = DateTime.UtcNow;
                custumerOrders.CartTotal = orderDetails.CartTotal;

                foreach (var order in orderDetails.OrderDetails)
                {
                    CustomerOrderDetails customerOrderDetails1 = new CustomerOrderDetails
                    {
                        OrderId = custumerOrders.OrderId,
                        ProductId = order.Game.GameId,
                        Quantity = order.Quantity,
                        Price =  order.Game.Price ,
                        TotalPrice=order.Quantity * order.Game.Price,
                        CoverFileName=order.Game.CoverFileName,
                    };

                    custumerOrders.CartTotal +=customerOrderDetails1.TotalPrice;                 
                    UnitOfWork.CustomerOrderDetailsRepository.Insert(customerOrderDetails1);
                }

                UnitOfWork.CustomerOrdersRepository.Insert(custumerOrders);
                UnitOfWork.Save();
            
            }
            catch
            {
                throw;
            }       
        }

        public List<CustomerOrders> GetListCustumerOrderUser(int userId)
        {
            return GetAllOrdersOfUser(userId);
        }

        public List<OrdersUserDto> GetOrdenUserDto(int userId)
        {
            try
            {                
                List<CustomerOrders> orders = GetAllOrdersOfUser(userId);
                List<OrdersUserDto> userOrderslistDto  = new List<OrdersUserDto>();
                foreach (CustomerOrders order in orders)
                {                  
                    List<CustomerOrderDetails> ordersDetails = GetAllOrdersDetailsOfUser(order.OrderId);
                    OrdersUserDto userOrderDto = new OrdersUserDto
                    {
                        CustomerOrderDetails = ordersDetails,
                        DateCreated = order.DateCreated,
                    };
                    userOrderslistDto.Add(userOrderDto);
                }
                return userOrderslistDto;
            }
            catch
            {
                throw;
            }
           
        }
        
        #region Helper

        private int CreateRandomNumber(int length)
        {
            Random rnd = new Random();
            return rnd.Next(Convert.ToInt32(Math.Pow(10, length - 1)), Convert.ToInt32(Math.Pow(10, length)));
        }

        private List<CustomerOrderDetails> ListAllCustomerOrdersDetailsFromDatabase()
        {
            return UnitOfWork.CustomerOrderDetailsRepository.Get().ToList();            
        }

        private List<CustomerOrderDetails> GetAllOrdersDetailsOfUser(string orderId)
        {
            return UnitOfWork.CustomerOrderDetailsRepository
                    .Get()
                    .Where(x => x.OrderId == orderId)
                    .ToList();            
        }

        private List<CustomerOrders> ListAllCustumerOrdersFromDatabase()
        {
            return UnitOfWork.CustomerOrdersRepository.Get().ToList();            
        }

        private List<CustomerOrders> GetAllOrdersOfUser(int userId)
        {
            return UnitOfWork.CustomerOrdersRepository
                    .Get()
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(b => b.DateCreated)
                    .ToList();                
        }
     
        #endregion
    }
}
