using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.TakuGames.Model.ViewModels
{
    public class UserMasterVM
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int UserTypeId { get; set; }
        public string UserImage {get;set;}
    }
}