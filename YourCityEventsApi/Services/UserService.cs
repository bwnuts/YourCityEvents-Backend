using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using YourCityEventsApi.Model;


namespace YourCityEventsApi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModel> _users;

        public UserService(IDatabaseSettings settings)
        {
            var client=new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<UserModel>("Users");
        }

        public List<UserModel> Get() =>
            _users.Find(user => true).ToList();

        public UserModel Get(string id) =>
            _users.Find(user => user.Id == id).FirstOrDefault();

        public UserModel Create(UserModel userModel)
        {
            _users.InsertOne(userModel);
            return userModel;
        }

        public void Update(string id, UserModel userModel) =>
            _users.ReplaceOne(user => user.Id == id, userModel);
        
        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public bool CheckPassword(string id, string password)
        {
            UserModel user = Get(id);
            return user.Password.Equals(password);
        }

        public UserModel GetByName(string userName)
        {
            return _users.Find(user => user.UserName == userName)
                .FirstOrDefault();
        }
        /*public async Task<bool> CheckPassword(UserModel user, string password)
        {
            bool ret= user.Password.Equals(password);
            return ret;
        }

        public async Task<UserModel> GetUserByName(string userName)
        {
            var filter = Builders<UserModel>.Filter.Eq("UserName", userName);
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }#1#*/
        
        /*public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var filter = Builders<UserModel>.Filter.Eq("Id", id);
            return await _context.Users
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<CreateUserResponse> CreateUser(UserModel user)
        {
            await _context.Users.InsertOneAsync(user);
            return new CreateUserResponse(user.Id, true);
        }

        public async Task<DeleteResult> RemoveUser(string id)
        {
            return await _context.Users.DeleteOneAsync(
                Builders<UserModel>.Filter.Eq("Id", id));
        }

        public async Task<ReplaceOneResult> UpdateUser(string id, UserModel user)
        {
            return await _context.Users
                .ReplaceOneAsync(u => u.Id.Equals(id), user
                    , new UpdateOptions {IsUpsert = true});
        }

        public async Task<bool> CheckPassword(UserModel user, string password)
        {
            bool ret= user.Password.Equals(password);
            return ret;
        }

        public async Task<UserModel> GetUserByName(string userName)
        {
            var filter = Builders<UserModel>.Filter.Eq("UserName", userName);
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }*/
    }
}