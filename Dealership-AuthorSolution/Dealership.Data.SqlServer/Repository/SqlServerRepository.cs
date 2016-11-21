using System.Collections.Generic;
using System.Linq;

using Dealership.Data.Contracts;
using Dealership.Data.SqlServer.Models;
using System.Data.Entity;

namespace Dealership.Data.SqlServer.Repository
{
    public class SqlServerRepository : IRepository<SqlServerUser>
    {
        private readonly DealershipDbContext context;
        private readonly IDbSet<SqlServerUser> dbSet;

        public SqlServerRepository(DealershipDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<SqlServerUser>();
        }

        public void Add(SqlServerUser entity)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public IEnumerable<SqlServerUser> All()
        {
            return this.dbSet.ToList();
        }

        public SqlServerUser FindByUsername(string username)
        {
            return this.dbSet.FirstOrDefault(u => u.Username == username);
        }

        public void Update(SqlServerUser entity)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
