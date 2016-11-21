using System;
using System.Collections.Generic;
using System.Text;

using Dealership.Common;
using Dealership.Data.Contracts;
using Dealership.Data.Common.Enums;
using Dealership.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Data.SqlServer.Models
{
    public class SqlServerUser : IUser
    {
        private const string UsernameProperty = "Username";
        private const string FirstNameProperty = "Firstname";
        private const string LastNameProperty = "Lastname";
        private const string PasswordProperty = "Password";
        private const string NoVehiclesHeader = "--NO VEHICLES--";
        private const string UserHeader = "--USER {0}--";

        public SqlServerUser()
        {

        }

        public SqlServerUser(string username, string firstName, string lastName, string password, string role)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = (Role)Enum.Parse(typeof(Role), role);
            //this.Vehicles = new List<IVehicle>();
            this.MongoCars = new List<SqlServerCar>();
            this.MongoTrucks = new List<SqlServerTruck>();
            this.MongoMotorcycles = new List<SqlServerMotorcycle>();

            this.ValidateFields();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public virtual List<SqlServerCar> MongoCars { get; set; }

        public virtual List<SqlServerTruck> MongoTrucks { get; set; }

        public virtual List<SqlServerMotorcycle> MongoMotorcycles { get; set; }

        [NotMapped]
        public IList<IVehicle> Vehicles
        {
            get
            {
                var vehicles = new List<IVehicle>();
                vehicles.AddRange(this.MongoCars);
                vehicles.AddRange(this.MongoTrucks);
                vehicles.AddRange(this.MongoMotorcycles);

                return vehicles;
            }
        }

        public void AddComment(IComment commentToAdd, IVehicle vehicleToAddComment)
        {
            Validator.ValidateNull(commentToAdd, Constants.CommentCannotBeNull);
            Validator.ValidateNull(vehicleToAddComment, Constants.CommentCannotBeNull);

            var sqlServerVehicle = vehicleToAddComment as SqlServerVehicle;
            sqlServerVehicle.SqlServerComments.Add(commentToAdd as SqlServerComment);
        }

        public void AddVehicle(IVehicle vehicle)
        {
            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);
            if (this.Role == Role.Normal && this.Vehicles.Count >= 5)
            {
                throw new ArgumentException(string.Format(Constants.NotAnVipUserVehiclesAdd, Constants.MaxVehiclesToAdd));
            }

            if (this.Role == Role.Admin)
            {
                throw new ArgumentException(Constants.AdminCannotAddVehicles);
            }

            this.Vehicles.Add(vehicle);
        }

        public void RemoveComment(IComment commentToRemove, IVehicle vehicleToRemoveComment)
        {
            Validator.ValidateNull(vehicleToRemoveComment, Constants.VehicleCannotBeNull);
            Validator.ValidateNull(commentToRemove, Constants.CommentCannotBeNull);

            if (this.Username != commentToRemove.Author)
            {
                throw new ArgumentException(Constants.YouAreNotTheAuthor);
            }

            vehicleToRemoveComment.Comments.Remove(commentToRemove);
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            Validator.ValidateNull(vehicle, Constants.VehicleCannotBeNull);

            this.Vehicles.Remove(vehicle);
        }

        public override string ToString()
        {
            return string.Format(Constants.UserToString, this.Username, this.FirstName, this.LastName, this.Role);
        }

        public string PrintVehicles()
        {
            var builder = new StringBuilder();

            var counter = 1;
            builder.AppendLine(string.Format(UserHeader, this.Username));

            if (this.Vehicles.Count <= 0)
            {
                builder.AppendLine(NoVehiclesHeader);
            }
            else
            {
                foreach (var vehicle in this.Vehicles)
                {
                    builder.AppendLine(string.Format("{0}. {1}", counter, vehicle.ToString()));
                    counter++;
                }
            }

            return builder.ToString().Trim();
        }

        private void ValidateFields()
        {
            Validator.ValidateNull(this.Username, string.Format(Constants.PropertyCannotBeNull, UsernameProperty));
            Validator.ValidateSymbols(this.Username, Constants.UsernamePattern, string.Format(Constants.InvalidSymbols, UsernameProperty));
            Validator.ValidateIntRange(this.Username.Length, Constants.MinNameLength, Constants.MaxNameLength, string.Format(Constants.StringMustBeBetweenMinAndMax, UsernameProperty, Constants.MinNameLength, Constants.MaxNameLength));

            Validator.ValidateNull(this.FirstName, string.Format(Constants.PropertyCannotBeNull, FirstNameProperty));
            Validator.ValidateIntRange(this.FirstName.Length, Constants.MinNameLength, Constants.MaxNameLength, string.Format(Constants.StringMustBeBetweenMinAndMax, FirstNameProperty, Constants.MinNameLength, Constants.MaxNameLength));

            Validator.ValidateNull(this.LastName, string.Format(Constants.PropertyCannotBeNull, LastNameProperty));
            Validator.ValidateIntRange(this.LastName.Length, Constants.MinNameLength, Constants.MaxNameLength, string.Format(Constants.StringMustBeBetweenMinAndMax, LastNameProperty, Constants.MinNameLength, Constants.MaxNameLength));

            Validator.ValidateNull(this.Password, string.Format(Constants.PropertyCannotBeNull, PasswordProperty));
            Validator.ValidateSymbols(this.Password, Constants.PasswordPattern, string.Format(Constants.InvalidSymbols, PasswordProperty));
            Validator.ValidateIntRange(this.Password.Length, Constants.MinPasswordLength, Constants.MaxPasswordLength, string.Format(Constants.StringMustBeBetweenMinAndMax, PasswordProperty, Constants.MinPasswordLength, Constants.MaxPasswordLength));
        }
    }
}
