using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Newtonsoft.Json;
using StackExchange.Redis;
using YourCityEventsApi.Model;


namespace YourCityEventsApi.ScheduleTask
{
    public class SyncDataService:BackgroundService
    {
        private IMongoCollection<UserModel> _users;
        private IMongoCollection<EventModel> _events;
        private IMongoCollection<CityModel> _cities;
        private IDatabase _redisUsersDatabase;
        private IDatabase _redisEventsDatabase;
        private IDatabase _redisCitiesDatabase;

        public SyncDataService(IMongoSettings mongoSettings, IRedisSettings redisSettings)
        {
            var client=new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _users = database.GetCollection<UserModel>("Users");
            _events = database.GetCollection<EventModel>("Events");
            _cities = database.GetCollection<CityModel>("Cities");
            
            ConfigurationOptions options = new ConfigurationOptions()
            {    
                EndPoints = {{redisSettings.Host,redisSettings.Port}},
                Password = redisSettings.Password
            };
            ConnectionMultiplexer redis=ConnectionMultiplexer.Connect(options);
            //var server = redis.GetServer(redisSettings.Host);
            /*server.FlushDatabase(0);
            server.FlushDatabase(1);
            server.FlushDatabase(2);*/
            _redisUsersDatabase = redis.GetDatabase(0);
            _redisEventsDatabase = redis.GetDatabase(1);
            _redisCitiesDatabase = redis.GetDatabase(2);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TimeSpan ttl = new TimeSpan(0,1,59,0);

            var allUsers = _users.Find(u => true).ToList();
            var allEvents = _events.Find(e => true).ToList();
            var allCities = _cities.Find(c => true).ToList();

            /*_redisUsersDatabase.StringSet("users", JsonConvert.SerializeObject(allUsers)
                , new TimeSpan(0,1,59,0));
            _redisEventsDatabase.StringSet("events", JsonConvert.SerializeObject(allEvents)
                , new TimeSpan(0,1,59,0));
            _redisCitiesDatabase.StringSet("cities", JsonConvert.SerializeObject(allCities)
                , new TimeSpan(0, 1, 59, 0));*/

            foreach (var user in allUsers)
            {
                _redisUsersDatabase.StringSet(user.Id, JsonConvert.SerializeObject(user), ttl);
            }

            foreach (var Event in allEvents)
            {
                _redisEventsDatabase.StringSet(Event.Id, JsonConvert.SerializeObject(Event), ttl);
            }

            foreach (var city in allCities)
            {
                _redisCitiesDatabase.StringSet(city.Id, JsonConvert.SerializeObject(city), ttl);
            }
            
            await Task.Delay(new TimeSpan(0, 2, 0, 0),stoppingToken);
        }
    }
}