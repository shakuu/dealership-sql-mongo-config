using System;
using System.Collections.Generic;

using Dealership.Data.Contracts;
using MongoDB.Driver;

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
            throw new NotImplementedException();
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
