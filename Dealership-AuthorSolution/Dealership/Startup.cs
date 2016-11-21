﻿using System.Reflection;

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

            //var repo = new MongoGenericRepository<MongoUser>();

            //repo.Add(user);
            //var all = repo.All();

            //System.Console.WriteLine(string.Join(",", all));

            var ninject = new StandardKernel();
            ninject.Load(Assembly.GetExecutingAssembly());

            var engine = ninject.Get<IEngine>();
            engine.Start();
        }
    }
}
