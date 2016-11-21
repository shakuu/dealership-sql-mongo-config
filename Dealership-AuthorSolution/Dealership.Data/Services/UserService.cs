using System.Collections.Generic;
using System.Linq;

using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;
using Dealership.Data.Services.Contracts;
using Dealership.Data.Factories;

namespace Dealership.Data.Services
{
    public class HashSetUserService : IUserService
    {
        private readonly ICollection<IUser> users;
        private readonly IDealershipFactory dealershipFactory;

        private IUser loggedUser;

        public HashSetUserService(IDealershipFactory dealershipFactory)
        {
            this.dealershipFactory = dealershipFactory;
            this.users = new HashSet<IUser>();
        }

        public IUser LoggedUser
        {
            get
            {
                return this.loggedUser;
            }
        }

        public void AddUserComment(string content, int vehicleIndex)
        {
            var comment = this.dealershipFactory.CreateComment(content);
            this.loggedUser.Vehicles[vehicleIndex].Comments.Add(comment);
        }

        public void AddUserVehicle(string make, string model, decimal price, string details, VehicleType type)
        {
            var vehicle = this.dealershipFactory.GetVehicle(type.ToString(), make, model, price, details);
            this.loggedUser.Vehicles.Add(vehicle);
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, string role)
        {
            var user = this.dealershipFactory.CreateUser(username, firstName, lastName, password, role);
            this.users.Add(user);

            return user;
        }

        public IEnumerable<IUser> FindAll()
        {
            return new List<IUser>(this.users);
        }
        
        public IUser FindByUsername(string username)
        {
            var user = this.users.FirstOrDefault(u => u.Username == username);

            return user;
        }

        public void RemoveUserComment(int vehicleIndex, int commentIndex)
        {
            this.loggedUser.Vehicles[vehicleIndex].Comments.RemoveAt(commentIndex);
        }

        public void RemoveUserVehicle(int vehicleIndex)
        {
            this.loggedUser.Vehicles.RemoveAt(vehicleIndex);
        }

        public void SetLoggedUser(IUser user)
        {
            this.loggedUser = user;
        }
    }
}
