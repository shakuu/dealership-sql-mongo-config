﻿using Dealership.Common;
using Dealership.Data.Common;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Models
{
    public class MongoCar : MongoVehicle, ICar, IMongoDbId
    {
        private const string SeatsProperty = "Seats";

        public MongoCar(string make, string model, decimal price, string details)
            : base(make, model, price, VehicleType.Car)
        {
            this.Seats = int.Parse(details);

            this.ValidateFields();
        }
        
        public int Seats { get; set; }

        protected override string PrintAdditionalInfo()
        {
            return string.Format("  {0}: {1}", SeatsProperty, this.Seats);
        }

        private void ValidateFields()
        {
            Validator.ValidateIntRange(this.Seats, Constants.MinSeats, Constants.MaxSeats, string.Format(Constants.NumberMustBeBetweenMinAndMax, SeatsProperty, Constants.MinSeats, Constants.MaxSeats));
        }
    }
}
