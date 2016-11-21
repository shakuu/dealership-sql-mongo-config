using Dealership.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Services
{
    public class MongoDbUserService : IUserService
    {
        private IUser loggedUser;

        public IUser LoggedUser
        {
            get
            {
                return this.loggedUser;
            }
        }

        public void AddUserComment(string content, int vehicleIndex)
        {
            throw new NotImplementedException();
        }

        public void AddUserVehicle(string make, string model, decimal price, string additional, VehicleType type)
        {
            throw new NotImplementedException();
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> FindAll()
        {
            throw new NotImplementedException();
        }

        public IUser FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserComment(int vehicleIndex, int commentIndex)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserVehicle(int vehicleIndex)
        {
            throw new NotImplementedException();
        }

        public void SetLoggedUser(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
