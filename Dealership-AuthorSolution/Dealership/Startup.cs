using System.Reflection;

using Dealership.Engine;

using Ninject;
using MongoDB.Driver;
using Dealership.Data.Models;

namespace Dealership
{
    public class Startup
    {
        public static void Main()
        {
            //var client = new MongoClient("mongodb://localhost:27017");
            //var db = client.GetDatabase("dealership");

            //var collection = db.GetCollection<User>("users");

            //var user = new User("Pesho", "Peshev", "Peshev", "123456", "Normal");
            //collection.InsertOne(user);

            var ninject = new StandardKernel();
            ninject.Load(Assembly.GetExecutingAssembly());

            var engine = ninject.Get<IEngine>();
            engine.Start();
        }
    }
}
