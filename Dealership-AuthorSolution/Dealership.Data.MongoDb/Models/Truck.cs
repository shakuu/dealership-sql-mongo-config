using Dealership.Data.Contracts;
using Dealership.Data.MongoDb.Models;

namespace Dealership.Data.Models
{
    public class MongoTruck : MongoVehicle, ITruck
    {
        private const string WeightCapacityPropery = "Weight capacity";

        private readonly int weightCapacity;

        public MongoTruck()
        {
        }

        public int WeightCapacity { get; set; }

        protected override string PrintAdditionalInfo()
        {
            // Е mnoo sum gaden! Za edna glavna bukva! ЦЦЦЦЦЦЦ :D
            return string.Format("  Weight Capacity: {0}t", this.WeightCapacity);
        }
    }
}
