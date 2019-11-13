using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using YourCityEventsApi.Model;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;

namespace YourCityEventsApi.UserService
{
    public interface IUserModelRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(string id);

        Task<CreateUserResponse> CreateUser(UserModel user);

        Task<DeleteResult> RemoveUser(string id);

        Task<ReplaceOneResult> UpdateUser(string id, UserModel user);

        Task<bool> CheckPassword(UserModel user, string password);

        Task<UserModel> GetUserByName(string userName);
    }
}