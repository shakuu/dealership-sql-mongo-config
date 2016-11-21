using System.Reflection;

using Dealership.Engine;

using Ninject;

namespace Dealership
{
    public class Startup
    {
        public static void Main()
        {
            //var client = new MongoClient("mongodb://localhost:27017");
            //var db = client.GetDatabase("dealership");

            //var user = new MongoUser("Gosho", "Peshev", "Peshev", "123456", "Normal");

            //var repo = new MongoUserRepository();
            //repo.Add(user);

            //var pesho = repo.FindByUsername("Gosho");
            //MongoVehicle car = new MongoCar("make", "model", 10000, "5");

            //pesho.AddVehicle(car);
            //repo.Update(pesho);

            //var comment = new MongoComment("content");
            //car.MongoComments.Add(comment);
            //repo.Update(pesho);

            var ninject = new StandardKernel();
            ninject.Load(Assembly.GetExecutingAssembly());

            var engine = ninject.Get<IEngine>();
            engine.Start();
        }
    }
}
