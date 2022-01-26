using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dal;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.TakuGames.Business
{
    public class OrderBusiness: BaseBusiness, IOrderBusiness
    {
        public OrderBusiness(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<OrderBusiness> logger): base(unitOfWork, mapper, logger
            ){}

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
            return GetAllCustumerOrdersWithUserId(userId);
        }
        public List<OrdersUserDto> GetOrdenUserDto(int userId)
        {
            try
            {                
                List<CustomerOrders> custumerOrdersList = GetAllCustumerOrdersWithUserId(userId);
                List<OrdersUserDto> orderUserList = new List<OrdersUserDto>();
                foreach (CustomerOrders item in custumerOrdersList)
                {                  
                    List<CustomerOrderDetails> custumerOrdersDetailsList = GetAllCustomerOrderDetailsWithUserId(item.OrderId);
                    OrdersUserDto orderUserDto = new OrdersUserDto
                    {
                        CustomerOrderDetails = custumerOrdersDetailsList,
                        DateCreated = item.DateCreated,
                    };
                    orderUserList.Add(orderUserDto);
                }
                return orderUserList;
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
            var resp = UnitOfWork.CustomerOrderDetailsRepository.Get().ToList();
            return resp;
        }
        private List<CustomerOrderDetails> GetAllCustomerOrderDetailsWithUserId(string orderId)
        {
            return ListAllCustomerOrdersDetailsFromDatabase()
                .Where(x => x.OrderId == orderId)
                .ToList();
        }
        private List<CustomerOrders> ListAllCustumerOrdersFromDatabase()
        {
            return  UnitOfWork.CustomerOrdersRepository.Get().ToList();
            
        }
        private List<CustomerOrders> GetAllCustumerOrdersWithUserId(int userId)
        {
            return ListAllCustumerOrdersFromDatabase()
                    .Where( x => x.UserId == userId )
                    .OrderByDescending( x => x.DateCreated )
                    .ToList();
        }
        
        #endregion
    }
}
