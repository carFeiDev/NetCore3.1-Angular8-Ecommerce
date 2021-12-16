using System;

namespace Project.TakuGames.Model.Domain
{
    public partial class Cart
    {
        public string CartId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
