using System;
using PetShop.Domain.Entities;

namespace PetShop.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
