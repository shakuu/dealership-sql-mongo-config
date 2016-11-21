using Dealership.Data.SqlServer.Repository.Contracts;

namespace Dealership.Data.SqlServer.Repository
{
    public class DealershipUnitOfWork : IUnitOfWork
    {
        private readonly DealershipDbContext context;

        public DealershipUnitOfWork(DealershipDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
