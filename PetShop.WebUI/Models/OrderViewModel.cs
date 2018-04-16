using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.WebUI.Models
{
    [Serializable]
    public class OrderViewModel
    {
        public int Id { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}