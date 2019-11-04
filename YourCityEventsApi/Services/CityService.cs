using MongoDB.Driver;
using System.Collections.Generic;
using YourCityEventsApi.Model;

namespace YourCityEventsApi.Services
{
    public class CityService
    {
        private IMongoCollection<CityModel> _cities;

        public CityService(IDatabaseSettings settings)
        {
            var client=new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cities = database.GetCollection<CityModel>("Cities");
        }

        public List<CityModel> Get()
        {
            return _cities.Find(city => true).ToList();
        }

        public CityModel Get(string id) =>
            _cities.Find(city => city.Id == id).FirstOrDefault();

        public CityModel GetByNameUa(string cityNameUa)
        {
            return _cities.Find(city => city.NameUa == cityNameUa)
                .FirstOrDefault();
        }
        
        public CityModel GetByNameEn(string cityNameEn)
        {
            return _cities.Find(city => city.NameEn == cityNameEn)
                .FirstOrDefault();
        }

        public CityModel Create(CityModel cityModel)
        {
            _cities.InsertOne(cityModel);
            return GetByNameUa(cityModel.NameUa);
        }

        public void Update(string id, CityModel cityModel) =>
            _cities.ReplaceOne(city => city.Id == id, cityModel);
        
        public void Delete(string id) =>
            _cities.DeleteOne(city => city.Id == id);
        
    }
}