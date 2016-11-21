using System.Collections.Generic;
using System.Linq;

using Dealership.Data.Contracts;
using Dealership.Data.MongoDb.Models;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Dealership.Data.MongoDb.Repository
{
    public class MongoUserRepository : IRepository<MongoUser>
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoCollection<MongoUser> collection;

        public MongoUserRepository()
            : this(new MongoClient("mongodb://localhost:27017"))
        {

        }

        public MongoUserRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            this.collection = this.mongoClient
                .GetDatabase("dealership")
                .GetCollection<MongoUser>(typeof(MongoUser).Name);
        }

        public void Add(MongoUser entity)
        {
            this.collection.InsertOne(entity);
        }

        public IEnumerable<MongoUser> All()
        {
            var serializedCollection = this.collection.Find(new BsonDocument()).ToList();

            return serializedCollection;
        }

        public MongoUser FindByUsername(string username)
        {
            var filter = Builders<MongoUser>.Filter.Eq("Username", username);
            var found = this.collection.Find(filter).ToList();

            return found.FirstOrDefault();
        }

        public void Update(MongoUser entity)
        {
            var filter = Builders<MongoUser>.Filter.Eq("_id", entity.Id);
            var update = Builders<MongoUser>.Update
                .Set("Vehicles", entity.Vehicles);

            var result = this.collection.UpdateOne(filter, update);
        }
    }
}
