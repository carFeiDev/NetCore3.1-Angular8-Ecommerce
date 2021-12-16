using System;
using System.Collections.Generic;

namespace Project.TakuGames.Model.Dto
{
     public class OrdersDto
    {
        public string OrderId { get; set; }
        public List<CartItemDto> OrderDetails { get; set; }
        public decimal CartTotal { get; set; }
        public DateTime OrderDate { get; set; }
    }
}