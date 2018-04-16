using System.Collections.Generic;
using AutoMapper;
using PetShop.BLL.DTO;
using PetShop.BLL.Infrastructure;
using PetShop.BLL.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Interfaces;

namespace PetShop.BLL.Services
{
    /// <summary>
    /// Represents service to manage orders.
    /// </summary>
    public class OrderService: IOrderService
    {
        private IUnitOfWork Db { get; }
        public OrderService(IUnitOfWork db)
        {
            Db = db;
        }
        /// <summary>
        /// Creates an entry for an order in data source.
        /// </summary>
        /// <param name="orderDto">Order</param>
        public void MakeOrder(OrderDTO orderDto)
        {
            foreach (var product in orderDto.Products)
            {
                var item = Db.Products.GetById(product.Id);
                if(item == null) throw new ValidationException($"Not found product with id-{product.Id}", "Id");
            }
            var order = new Order();
            var id = order.Id;
            foreach (var item in orderDto.Products)
            {
                order = new Order()
                {
                    Id = id,
                    Product = item,
                    ProductId = item.Id
                };
                
                Db.Orders.Create(order);
                Db.Save();
            }
        }
        /// <summary>
        /// Gets order by id.
        /// </summary>
        /// <param name="id">Order's id</param>
        /// <returns>OrderDTO</returns>
        public OrderDTO GetOrder(int? id)
        {
            if (id == null) throw new ValidationException($"Not set order's id", "Id");
            var order = Db.Orders.GetById(id.Value);
            if (order == null) throw new ValidationException($"Not found order with id-{id}", "Id");

            return Mapper.Map<Order, OrderDTO>(order);
        }
        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>Enumeration of all orders in datasource.</returns>
        public IEnumerable<OrderDTO> GetOrders()
        {
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Db.Orders.GetAll());
        } 
        /// <summary>
        /// Creates product to datasource.
        /// </summary>
        /// <param name="productDto">Creating product.</param>
        public void CreateProduct(ProductDTO productDto)
        {
            var product = Mapper.Map<ProductDTO, Product>(productDto);
            Db.Products.Create(product);
            Db.Save();
        }
        /// <summary>
        /// Deletes product from datasourse by id.
        /// </summary>
        /// <param name="id">Id deleting object.</param>
        public void DeleteProduct(int id)
        {
            Db.Products.Delete(id);
            Db.Save();
        }
        /// <summary>
        /// Updates product in datasource.
        /// </summary>
        /// <param name="productDto">Updating object.</param>
        public void UpdateProduct(ProductDTO productDto)
        {
            var product = Mapper.Map<ProductDTO, Product>(productDto);
            Db.Products.Update(product);
            Db.Save();
        }
        /// <summary>
        /// Gets product from datasource by id.
        /// </summary>
        /// <param name="id">Id getting product.</param>
        /// <returns>Product</returns>
        public ProductDTO GetProduct(int? id)
        {
            if(id == null) throw new ValidationException($"Not set product's id","Id");
            var product = Db.Products.GetById(id.Value);
            if(product == null) throw new ValidationException($"Not found product with id-{id}", "Id");

            return Mapper.Map<Product, ProductDTO>(product);
        }
        /// <summary>
        /// Gets all product from datasource.
        /// </summary>
        /// <returns>Enumeration of all products in datasource.</returns>
        public IEnumerable<ProductDTO> GetProducts()
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Db.Products.GetAll());
        }
        /// <summary>
        /// Disposes datasource.
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
