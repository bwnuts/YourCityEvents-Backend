using System.Linq;
using System.Threading.Tasks;
using YourCityEventsApi.Model;
using YourCityEventsApi.Model.AuthModels.UseCaseRequests;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;
using YourCityEventsApi.UseCases.Interfaces;
using YourCityEventsApi.UserService;
using YourCityEventsApi.UserService.Interfaces;

namespace YourCityEventsApi.UseCases
{
    public class RegisterUserUseCase: IRegisterUserUseCase
    {
        private readonly IUserModelRepository _userModelRepository;

        public RegisterUserUseCase(IUserModelRepository userModelRepository)
        {
            _userModelRepository = userModelRepository;
        }

        public async Task<bool> Handle(RegisterUserRequest message,
            IOutputPort<RegisterUserResponse> outputPort)
        {
            var response = await _userModelRepository
                .CreateUser(message.NewUser);
            outputPort.Handle(response.Success
                ? new RegisterUserResponse(response.Id, true)
                : new RegisterUserResponse(response.Errors.Select(e=>e.Description)));
            return response.Success;
        }
    }
}