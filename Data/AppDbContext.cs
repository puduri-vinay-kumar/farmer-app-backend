using FarmerApp.Api.Models;
using MongoDB.Driver;

namespace FarmerApp.Api.Data
{
    public class AppDbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDb:ConnectionString"]
                ?? throw new InvalidOperationException("MongoDb:ConnectionString is not configured.");
            var databaseName = configuration["MongoDb:DatabaseName"]
                ?? throw new InvalidOperationException("MongoDb:DatabaseName is not configured.");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");
    }
}
