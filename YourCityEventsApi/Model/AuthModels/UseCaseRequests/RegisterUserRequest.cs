using YourCityEventsApi.UserService.Interfaces;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;

namespace YourCityEventsApi.Model.AuthModels.UseCaseRequests
{
    public class RegisterUserRequest : IUseCaseRequest<RegisterUserResponse>
    {
        public UserModel NewUser { get; set; }

        public RegisterUserRequest(UserModel newUser)
        {
            NewUser = newUser;
        }
    }
}