﻿namespace PetShop.BLL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal PricePerItem { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
