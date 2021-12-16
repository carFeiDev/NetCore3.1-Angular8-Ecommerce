using Project.TakuGames.Model.Domain;
using System;
using System.Collections.Generic;

namespace Project.TakuGames.Model.ViewModels
{
    public class OrdersUserDtoVM
    {
        public List<CustomerOrderDetails> CustomerOrderDetails { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
