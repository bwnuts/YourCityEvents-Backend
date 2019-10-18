using System.ComponentModel;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace YourCityEventsApi.Model
{
    public class UserContext
    {
        private readonly IMongoDatabase _database = null;

        public UserContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<UserModel> Users
        {
            get { return _database.GetCollection<UserModel>("Users"); }
        }
    }
}