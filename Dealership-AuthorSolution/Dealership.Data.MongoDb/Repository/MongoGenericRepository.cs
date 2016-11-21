using System;
using System.Collections.Generic;

using Dealership.Data.Contracts;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using MongoDB.Bson.Serialization;
using Dealership.Data.MongoDb.Models;

namespace Dealership.Data.MongoDb.Repository
{
    public class MongoGenericRepository<T> : IRepository<T>
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoCollection<T> collection;

        public MongoGenericRepository()
            : this(new MongoClient("mongodb://localhost:27017"))
        {

        }

        public MongoGenericRepository(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
            this.collection = this.mongoClient
                .GetDatabase("dealership")
                .GetCollection<T>(typeof(T).Name);
        }

        public void Add(T entity)
        {
            this.collection.InsertOne(entity);
        }

        public IEnumerable<T> All()
        {
            var serializedCollection = this.collection.Find(new BsonDocument()).ToList();
          
            return serializedCollection;
        }

        public T FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
