using System.ComponentModel;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace YourCityEventsApi.Model
{
    public class DbContext
    {
        private readonly IMongoDatabase _database = null;
        
        public DbContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<UserModel> Users
        {
            get { return _database.GetCollection<UserModel>("Users"); }
            
        }
    }
}