﻿using System.Collections.Generic;

using Dealership.Data.Services.Contracts;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;
using Dealership.Data.MongoDb.Models;

namespace Dealership.Data.MongoDb.Services
{
    public class MongoDbUserService : IUserService
    {
        private readonly IRepository<MongoUser> mongoUsers;
        private readonly IDealershipFactory dealershipFactory;

        private MongoUser loggedUser;

        public MongoDbUserService(IRepository<MongoUser> mongoUsers, IDealershipFactory dealershipFactory)
        {
            this.mongoUsers = mongoUsers;
            this.dealershipFactory = dealershipFactory;
        }

        public IUser LoggedUser
        {
            get
            {
                return this.loggedUser;
            }

            set
            {
                this.loggedUser = (MongoUser)value;
            }
        }

        public void AddUserComment(string content, int vehicleIndex)
        {
            var comment = this.dealershipFactory.CreateComment(content);
            this.loggedUser.Vehicles[vehicleIndex].Comments.Add(comment);
            this.UpdateUser();
        }

        public void AddUserVehicle(string make, string model, decimal price, string details, VehicleType type)
        {
            var vehicle = this.dealershipFactory.GetVehicle(type.ToString(), make, model, price, details);
            this.loggedUser.Vehicles.Add(vehicle);
            this.UpdateUser();
        }

        public IUser CreateUser(string username, string firstName, string lastName, string password, string role)
        {
            var user = this.dealershipFactory.CreateUser(username, firstName, lastName, password, role);
            this.mongoUsers.Add((MongoUser)user);

            return user;
        }

        public IEnumerable<IUser> FindAll()
        {
            return this.mongoUsers.All();
        }

        public IUser FindByUsername(string username)
        {
            var user = this.mongoUsers.FindByUsername(username);
            return user;
        }

        public void RemoveUserComment(int vehicleIndex, int commentIndex)
        {
            this.loggedUser.Vehicles[vehicleIndex].Comments.RemoveAt(commentIndex);
            this.UpdateUser();
        }

        public void RemoveUserVehicle(int vehicleIndex)
        {
            this.loggedUser.Vehicles.RemoveAt(vehicleIndex);
            this.UpdateUser();
        }

        public void SetLoggedUser(IUser user)
        {
            this.LoggedUser = user;
        }

        private void UpdateUser()
        {
            this.mongoUsers.Update(this.loggedUser);
        }
    }
}