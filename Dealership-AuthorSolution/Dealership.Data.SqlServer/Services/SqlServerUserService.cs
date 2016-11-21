using System.Collections.Generic;

using Dealership.Data.Services.Contracts;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;
using Dealership.Data.SqlServer.Models;
using Dealership.Data.SqlServer.Factories;

namespace Dealership.Data.SqlServer.Services
{
    public class SqlServerUserService : IUserService
    {
        private readonly IRepository<SqlServerUser> usersRepository;
        private readonly IDealershipFactory dealershipFactory;
        private readonly IUnitOfWorkFactory uowFactory;

        private SqlServerUser loggedUser;

        public SqlServerUserService(IRepository<SqlServerUser> usersRepository, IDealershipFactory dealershipFactory, IUnitOfWorkFactory uowFactory)
        {
            this.usersRepository = usersRepository;
            this.dealershipFactory = dealershipFactory;
            this.uowFactory = uowFactory;
        }

        public IUser LoggedUser
        {
            get
            {
                return this.loggedUser;
            }

            set
            {
                this.loggedUser = (SqlServerUser)value;
            }
        }

        public void AddUserComment(string content, string targetUsername, int vehicleIndex)
        {
            var comment = this.dealershipFactory.CreateComment(content);
            comment.Author = this.loggedUser.Username;

            using (var uow = this.uowFactory.CreateUnitOfWork())
            {
                var user = this.usersRepository.FindByUsername(targetUsername);
                user.Vehicles[vehicleIndex].Comments.Add(comment);

                this.usersRepository.Update(user);

                uow.Commit();
            }
        }

        public void AddUserVehicle(string make, string model, decimal price, string details, VehicleType type)
        {
            var vehicle = this.dealershipFactory.GetVehicle(type.ToString(), make, model, price, details);

            using (var uow = this.uowFactory.CreateUnitOfWork())
            {
                switch (type)
                {
                    case VehicleType.Motorcycle:
                        this.loggedUser.MongoMotorcycles.Add(vehicle as SqlServerMotorcycle);
                        break;
                    case VehicleType.Car:
                        this.loggedUser.MongoCars.Add(vehicle as SqlServerCar);
                        break;
                    case VehicleType.Truck:
                        this.loggedUser.MongoTrucks.Add(vehicle as SqlServerTruck);
                        break;
                    default:
                        break;
                }

                uow.Commit();
            }
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, string role)
        {
            var user = this.dealershipFactory.CreateUser(username, firstName, lastName, password, role);
            using (var uow = this.uowFactory.CreateUnitOfWork())
            {
                this.usersRepository.Add((SqlServerUser)user);

                uow.Commit();
            }

            return user;
        }

        public IEnumerable<IUser> FindAll()
        {
            return this.usersRepository.All();
        }

        public IUser FindByUsername(string username)
        {
            var user = this.usersRepository.FindByUsername(username);
            return user;
        }

        public void RemoveUserComment(int vehicleIndex, int commentIndex)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork())
            {
                this.loggedUser.Vehicles[vehicleIndex].Comments.RemoveAt(commentIndex);
            }
        }

        public void RemoveUserVehicle(int vehicleIndex)
        {
            using (var uow = this.uowFactory.CreateUnitOfWork())
            {
                var vehicleToRemove = this.loggedUser.Vehicles[vehicleIndex];

                switch (vehicleToRemove.Type)
                {
                    case VehicleType.Motorcycle:
                        this.loggedUser.MongoMotorcycles.Remove(vehicleToRemove as SqlServerMotorcycle);
                        break;
                    case VehicleType.Car:
                        this.loggedUser.MongoCars.Remove(vehicleToRemove as SqlServerCar);
                        break;
                    case VehicleType.Truck:
                        this.loggedUser.MongoTrucks.Remove(vehicleToRemove as SqlServerTruck);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SetLoggedUser(IUser user)
        {
            var sqlServer = this.usersRepository.FindByUsername(user.Username);

            this.LoggedUser = sqlServer;
        }
    }
}
