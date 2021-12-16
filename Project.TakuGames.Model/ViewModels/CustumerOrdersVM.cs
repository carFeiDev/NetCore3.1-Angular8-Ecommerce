using System;

namespace Project.TakuGames.Model.ViewModels
{
    public class CustumerOrdersVM
    {
        public string OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal CartTotal { get; set; }
    }
}
