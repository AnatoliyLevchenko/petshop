using System.Collections.Generic;
using PetShop.BLL.DTO;

namespace PetShop.BLL.Interfaces
{
    public interface IOrderService
    {
        void MakeOrder(OrderDTO orderDto);
        OrderDTO GetOrder(int? id);
        IEnumerable<OrderDTO> GetOrders();
        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> GetProducts();
        void DeleteProduct(int id);
        void UpdateProduct(ProductDTO productDto);
        void CreateProduct(ProductDTO productDto);
        void Dispose();
    }
}
