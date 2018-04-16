using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.WebUI.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

}