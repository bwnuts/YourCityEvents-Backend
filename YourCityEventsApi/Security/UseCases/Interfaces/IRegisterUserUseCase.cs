using YourCityEventsApi.Model.AuthModels.UseCaseRequests;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;
using YourCityEventsApi.UserService.Interfaces;

namespace YourCityEventsApi.UseCases.Interfaces
{
    public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest,RegisterUserResponse>

    {

    }
}