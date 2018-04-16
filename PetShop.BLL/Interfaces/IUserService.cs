using System.Collections.Generic;
using PetShop.BLL.DTO;

namespace PetShop.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        void DeleteUser(int id);
        void UpdateUser(UserDTO userDto);
        void CreateUser(UserDTO userDto);
        void Dispose();
    }
}
