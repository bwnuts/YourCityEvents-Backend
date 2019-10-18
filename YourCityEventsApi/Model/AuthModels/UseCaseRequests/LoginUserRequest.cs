using YourCityEventsApi.UserService.Interfaces;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;

namespace YourCityEventsApi.Model.AuthModels.UseCaseRequests
{
    public class LoginUserRequest : IUseCaseRequest<LoginUserResponse>
    {
        public string UserName { get; }
        public string Password { get; }

        public LoginUserRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}