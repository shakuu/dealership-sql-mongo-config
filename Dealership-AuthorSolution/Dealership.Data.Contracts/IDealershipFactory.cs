using Dealership.Data.Contracts;

namespace Dealership.Data.Contracts
{
    public interface IDealershipFactory
    {
        IUser CreateUser(string username, string firstName, string lastName, string password, string role);

        IComment CreateComment(string content);

        IVehicle GetVehicle(string type, string make, string model, decimal price, string details);
    }
}
