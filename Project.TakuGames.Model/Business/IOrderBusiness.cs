using Project.TakuGames.Model.Domain;
using System.Collections.Generic;
using Project.TakuGames.Model.Dto;

namespace Project.TakuGames.Model.Business
{
    public interface IOrderBusiness
    {
        List<CustomerOrders> GetListCustumerOrderUser(int userId);
        List<OrdersUserDto> GetOrdenUserDto(int userId);
        void CreateOrder(int userId, OrdersDto orderDetails);
    }
}
