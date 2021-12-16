using System;

namespace Project.TakuGames.Model.Domain
{
    public partial class Favoritelist
    {
        public string FavoritelistId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}