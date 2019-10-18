using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YourCityEventsApi.Model;
using System.Threading.Tasks;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;

namespace YourCityEventsApi.UserService
{
    public class UserModelRepository : IUserModelRepository
    {
        private readonly UserContext _context = null;

        public UserModelRepository(IOptions<Settings> settings)
        {
            _context=new UserContext(settings);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
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
        }
    }
}