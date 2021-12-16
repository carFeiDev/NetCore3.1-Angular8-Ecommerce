
using System.ComponentModel.DataAnnotations;

namespace Project.TakuGames.Model.Domain
{
    public class Games
    {
        [Key]
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        
    }
}

