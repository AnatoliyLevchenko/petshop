using System;
using PetShop.Domain.Entities;
using PetShop.Domain.EntityFramework;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Repositories
{
    /// <summary>
    /// Encapsulates the logic of working with data sources.
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly PetsShopContext _db;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private UserRepository _userRepository;
        /// <summary>
        /// Sets connection for repositories.
        /// </summary>
        /// <param name="connectionString">Connection string to datasource.</param>
        public EfUnitOfWork(string connectionString)
        {
            _db = new PetsShopContext(connectionString);
        }
        /// <summary>
        /// Represent Product repository  for current datasource.
        /// </summary>
        public IRepository<Product> Products
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_db)); }
        }
        /// <summary>
        /// Represent Order repository  for current datasource.
        /// </summary>
        public IRepository<Order> Orders
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new OrderRepository(_db));
            }
        }
        public IRepository<User> Users
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_db));
            }
        }
        /// <summary>
        /// Saves currently state of database.
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed;
        /// <summary>
        /// Dispose all managed resources.
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }
        /// <summary>
        /// Dispose all resources used this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
