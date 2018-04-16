using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PetShop.Domain.Entities;
using PetShop.Domain.EntityFramework;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly PetsShopContext _db;
        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="db">Context of data.</param>
        public ProductRepository(PetsShopContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all products from the context.
        /// </summary>
        /// <returns>Enumeration all products.</returns>
        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }
        /// <summary>
        /// Gets product by Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>Product.</returns>
        public Product GetById(int id)
        {
            return _db.Products.Find(id);
        }
        /// <summary>
        /// Finds all productss which satisfy the predicate.
        /// </summary>
        /// <param name="predicate">Predicate that selects products.</param>
        /// <returns>Products that satisfy the predicate.</returns>
        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return _db.Products.Where(predicate).ToList();
        }
        /// <summary>
        /// Creates entry in data source.
        /// </summary>
        /// <param name="item">Recorded object.</param>
        public void Create(Product item)
        {
            _db.Products.Add(item);
        }
        /// <summary>
        /// Updates an exsisted entry of product in data source.
        /// </summary>
        /// <param name="item">Object that updates.</param>
        public void Update(Product item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
        /// <summary>
        /// Removes an entry by id from data source.
        /// </summary>
        /// <param name="id">Identifier removable object.</param>
        public void Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null) _db.Products.Remove(product);
        }
    }
}
