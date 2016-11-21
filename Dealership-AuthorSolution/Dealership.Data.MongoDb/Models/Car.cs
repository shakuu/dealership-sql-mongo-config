using Dealership.Data.Contracts;

namespace Dealership.Data.MongoDb.Models
{
    public class MongoCar : MongoVehicle, ICar
    {
        private const string SeatsProperty = "Seats";

        private readonly int seats;

        public MongoCar()
        {
        }

        public int Seats { get; set; }

        protected override string PrintAdditionalInfo()
        {
            return string.Format("  {0}: {1}", SeatsProperty, this.Seats);
        }
    }
}
