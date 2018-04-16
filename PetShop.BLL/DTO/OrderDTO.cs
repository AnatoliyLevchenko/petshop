using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities;

namespace PetShop.BLL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
