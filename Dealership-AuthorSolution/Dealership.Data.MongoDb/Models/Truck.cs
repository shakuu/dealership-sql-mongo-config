using Dealership.Common;
using Dealership.Data.Common;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Models
{
    public class MongoTruck : MongoVehicle, ITruck
    {
        private const string WeightCapacityPropery = "Weight capacity";

        private readonly int weightCapacity;

        public MongoTruck(string make, string model, decimal price, string details)
            : base(make, model, price, VehicleType.Truck)
        {
            this.weightCapacity = int.Parse(details);

            this.ValidateFields();
        }

        public int WeightCapacity
        {
            get
            {
                return this.weightCapacity;
            }
        }

        protected override string PrintAdditionalInfo()
        {
            // Е mnoo sum gaden! Za edna glavna bukva! ЦЦЦЦЦЦЦ :D
            return string.Format("  Weight Capacity: {0}t", this.WeightCapacity);
        }

        private void ValidateFields()
        {
            Validator.ValidateIntRange(this.weightCapacity, Constants.MinCapacity, Constants.MaxCapacity, string.Format(Constants.NumberMustBeBetweenMinAndMax, WeightCapacityPropery, Constants.MinCapacity, Constants.MaxCapacity));
        }
    }
}
