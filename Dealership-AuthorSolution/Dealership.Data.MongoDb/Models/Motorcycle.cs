using Dealership.Data.Contracts;
using Dealership.Data.MongoDb.Models;

namespace Dealership.Data.Models
{
    public class MongoMotorcycle : MongoVehicle, IMotorcycle
    {
        private const string CategoryProperty = "Category";

        private readonly string category;

        public MongoMotorcycle()
        {
        }

        public string Category { get; set; }

        protected override string PrintAdditionalInfo()
        {
            return string.Format("  {0}: {1}", CategoryProperty, this.Category);
        }
    }
}
