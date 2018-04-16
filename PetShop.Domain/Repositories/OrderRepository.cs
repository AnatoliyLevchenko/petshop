using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PetShop.Domain.Entities;
using PetShop.Domain.EntityFramework;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Repositories
{
    class OrderRepository : IRepository<Order>
    {
        private readonly PetsShopContext _db;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="db">Context of data.</param>
        public OrderRepository(PetsShopContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all orders from the context.
        /// </summary>
        /// <returns>Enumeration all orders.</returns>
        public IEnumerable<Order> GetAll()
        {
            return _db.Orders;
        }
        /// <summary>
        /// Gets order by Id.
        /// </summary>
        /// <param name="id">Order Id.</param>
        /// <returns>Order.</returns>
        public Order GetById(int id)
        {
            return _db.Orders.Find(id);
        }
        /// <summary>
        /// Finds all orders which satisfy the predicate.
        /// </summary>
        /// <param name="predicate">Predicate that selects orders.</param>
        /// <returns>Orders that satisfy the predicate.</returns>
        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return _db.Orders.Where(predicate).ToList();
        }
        /// <summary>
        /// Creates entry in data source.
        /// </summary>
        /// <param name="item">Recorded object.</param>
        public void Create(Order item)
        {
            _db.Orders.Add(item);
        }

        /// <summary>
        /// Updates an exsisted entry in data source.
        /// </summary>
        /// <param name="item">Object that updates.</param>
        public void Update(Order item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
        
        /// <summary>
        /// Removes an entry by id from data source.
        /// </summary>
        /// <param name="id">Identifier removable object.</param>
        public void Delete(int id)
        {
            var order = _db.Orders.Find(id);
            if (order != null) _db.Orders.Remove(order);
        }
    }
}
