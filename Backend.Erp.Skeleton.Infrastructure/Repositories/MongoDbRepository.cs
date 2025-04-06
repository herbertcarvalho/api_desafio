using Backend.Erp.Skeleton.Domain.Helper;
using Backend.Erp.Skeleton.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Infrastructure.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument>
    where TDocument : IDocument
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(IConfiguration configuration)
        {
            var section = configuration.GetSection("MongoDb");
            var mongoConnection = Environment.GetEnvironmentVariable("CONNECTION_STRING_MONGO") ?? section["MongoDbConnection"];
            Console.WriteLine("@@@@@@@@@@@@@@@@@@@");
            Console.WriteLine(mongoConnection);
            _database = new MongoClient(section["MongoDbConnection"]).GetDatabase(section["DatabaseName"]);
        }

        public virtual Task InsertOneAsync(string collection, TDocument document)
        {
            return Task.Run(() => _database.GetCollection<TDocument>(collection).InsertOneAsync(document));
        }
    }
}
