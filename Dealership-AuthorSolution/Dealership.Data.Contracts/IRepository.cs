using System.Collections.Generic;

namespace Dealership.Data.Contracts
{
    public interface IRepository<T>
    {
        void Add(T entity);

        IEnumerable<T> All();

        T FindByUsername(string username);

        void Update(T entity);
    }
}
