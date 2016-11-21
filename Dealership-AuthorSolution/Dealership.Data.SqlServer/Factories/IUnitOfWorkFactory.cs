using Dealership.Data.SqlServer.Repository.Contracts;

namespace Dealership.Data.SqlServer.Factories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
