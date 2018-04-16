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
    /// Represents service to manage users.
    /// </summary>
    public class UserService: IUserService
    {
        private IUnitOfWork Db { get; }
        
        public UserService(IUnitOfWork db)
        {
            Db = db;
        }
        /// <summary>
        /// Gets user bu id.
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>UserDTO</returns>
        public UserDTO GetUser(int? id)
        {
            if (id == null) throw new ValidationException($"Not set user's id", "Id");
            var user = Db.Users.GetById(id.Value);
            if (user == null) throw new ValidationException($"Not found user with id-{id}", "Id");

            return Mapper.Map<User, UserDTO>(user);
        }
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Enumeration of all users in datasource.</returns>
        public IEnumerable<UserDTO> GetUsers()
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Db.Users.GetAll());
        }
        /// <summary>
        /// Deletes user from datasourse by id.
        /// </summary>
        /// <param name="id">Id deleting object.</param>
        public void DeleteUser(int id)
        {
            Db.Users.Delete(id);
            Db.Save();
        }
        /// <summary>
        /// Updates user in datasource.
        /// </summary>
        /// <param name="userDto">Updating object.</param>
        public void UpdateUser(UserDTO userDto)
        {
            var user = Mapper.Map<UserDTO, User>(userDto);
            Db.Users.Update(user);
            Db.Save();
        }
        /// <summary>
        /// Creates user to datasource.
        /// </summary>
        /// <param name="userDto">Creating user.</param>
        public void CreateUser(UserDTO userDto)
        {
            var user = Mapper.Map<UserDTO, User>(userDto);
            Db.Users.Create(user);
            Db.Save();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
