using System;

namespace Dealership.Data.SqlServer.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
