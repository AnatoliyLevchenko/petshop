using System;
using System.Collections.Generic;

namespace PetShop.Domain.Interfaces
{
    /// <summary>
    /// Interface for working with data, independetly of the data source
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
