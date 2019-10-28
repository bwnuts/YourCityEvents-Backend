using YourCityEventsApi.Model.AuthModels;
using System.Threading.Tasks;

namespace YourCityEventsApi.UserService
{
    public interface IJwtFactory
    {
        Task<TokenModel> GenerateEncodedToken(string id, string name);
    }
}