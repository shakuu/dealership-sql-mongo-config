using Dealership.Common;
using Dealership.Data.Common;
using Dealership.Data.Common.Enums;
using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Models
{
    public class MongoMotorcycle : MongoVehicle, IMotorcycle
    {
        private const string CategoryProperty = "Category";

        private readonly string category;

        public MongoMotorcycle(string make, string model, decimal price, string details)
            : base(make, model, price, VehicleType.Motorcycle)
        {
            this.category = details;

            this.ValidateFields();
        }

        public string Category
        {
            get
            {
                return this.category;
            }
        }

        protected override string PrintAdditionalInfo()
        {
            return string.Format("  {0}: {1}", CategoryProperty, this.Category);
        }

        private void ValidateFields()
        {
            Validator.ValidateNull(this.category, string.Format(Constants.PropertyCannotBeNull, CategoryProperty));
            Validator.ValidateIntRange(this.category.Length, Constants.MinCategoryLength, Constants.MaxCategoryLength, string.Format(Constants.StringMustBeBetweenMinAndMax, CategoryProperty, Constants.MinCategoryLength, Constants.MaxCategoryLength));
        }
    }
}
