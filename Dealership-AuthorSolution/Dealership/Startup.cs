using System.Reflection;

using Dealership.Engine;

using Ninject;
using MongoDB.Driver;
using Dealership.Data.Models;
using Dealership.Data.MongoDb.Models;
using Dealership.Data.MongoDb.Repository;

namespace Dealership
{
    public class Startup
    {
        public static void Main()
        {
            //var client = new MongoClient("mongodb://localhost:27017");
            //var db = client.GetDatabase("dealership");

            //var collection = db.GetCollection<User>("users");

            //var user = new MongoUser("Pesho", "Peshev", "Peshev", "123456", "Normal");
            ////collection.InsertOne(user);

            var repo = new MongoUserRepository();

            //var all = repo.All();

            var pesho = repo.FindByUsername("Pesho");
            var car = new MongoCar("make", "model", 10000, "5");
            pesho.Vehicles.Add(car);

            repo.Update(pesho);

            //var ninject = new StandardKernel();
            //ninject.Load(Assembly.GetExecutingAssembly());

            //var engine = ninject.Get<IEngine>();
            //engine.Start();
        }
    }
}
