using System.Data.Entity;

using Dealership.Data.SqlServer.Models;

namespace Dealership.Data.SqlServer
{
    public class DealershipDbContext : DbContext
    {
        public DealershipDbContext()
            : base("name=DealershipDb")
        {
        }

        IDbSet<SqlServerUser> Users { get; set; }

        IDbSet<SqlServerCar> Cars { get; set; }

        IDbSet<SqlServerTruck> Trucks { get; set; }

        IDbSet<SqlServerMotorcycle> Motorcycles { get; set; }

        IDbSet<SqlServerComment> Comments { get; set; }
    }
}
