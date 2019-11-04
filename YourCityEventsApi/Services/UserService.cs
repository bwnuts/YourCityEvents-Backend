using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourCityEventsApi.Model;


namespace YourCityEventsApi.Services
{
    public class UserService
    {
        private IMongoCollection<UserModel> _users;

        public UserService(IDatabaseSettings settings)
        {
            var client=new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<UserModel>("Users");
        }

        public List<UserModel> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public UserModel Get(string id) =>
            _users.Find(user => user.Id == id).FirstOrDefault();

        public UserModel GetByName(string userName)
        {
            return _users.Find(user => user.UserName == userName)
                .FirstOrDefault();
        }
        
        public UserModel Create(UserModel userModel)
        {
            _users.InsertOne(userModel);
            return GetByName(userModel.UserName);
        }

        public void Update(string id, UserModel userModel) =>
            _users.ReplaceOne(user => user.Id == id, userModel);
        
        public void Delete(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public bool CheckPassword(string id, string password)
        {
            UserModel user = Get(id);
            return user.Password.Equals(password);
        }
    }
}