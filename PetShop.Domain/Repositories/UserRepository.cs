using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Domain.Entities;
using PetShop.Domain.EntityFramework;
using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly PetsShopContext _db;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        /// <param name="db">Context of data.</param>
        public UserRepository(PetsShopContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Gets all users from the context.
        /// </summary>
        /// <returns>Enumeration all users.</returns>
        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }
        /// <summary>
        /// Gets user by Id.
        /// </summary>
        /// <param name="id">User's Id.</param>
        /// <returns>User.</returns>
        public User GetById(int id)
        {
            return _db.Users.Find(id);
        }
        /// <summary>
        /// Finds all users which satisfy the predicate.
        /// </summary>
        /// <param name="predicate">Predicate that selects users.</param>
        /// <returns>Users that satisfy the predicate.</returns>
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }
        /// <summary>
        /// Creates entry in data source.
        /// </summary>
        /// <param name="item">Recorded object.</param>
        public void Create(User item)
        {
            _db.Users.Add(item);
        }
        /// <summary>
        /// Updates an exsisted entry in data source.
        /// </summary>
        /// <param name="item">Object that updates.</param>
        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
        /// <summary>
        /// Removes an entry by id from data source.
        /// </summary>
        /// <param name="id">Identifier removable object.</param>
        public void Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null) _db.Users.Remove(user);
        }
    }
}
