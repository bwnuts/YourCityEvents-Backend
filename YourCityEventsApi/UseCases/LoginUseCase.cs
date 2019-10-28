using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;
using YourCityEventsApi.Model.AuthModels;
using YourCityEventsApi.Model.AuthModels.UseCaseRequests;
using YourCityEventsApi.Model.AuthModels.UseCaseResponses;
using YourCityEventsApi.UseCases.Interfaces;
using YourCityEventsApi.UserService;
using YourCityEventsApi.UserService.Interfaces;

namespace YourCityEventsApi.UseCases
{
    public class LoginUseCase : ILoginUserUseCase
    {
        private readonly IUserModelRepository _userModelRepository;
        private readonly IJwtFactory _jwtFactory;

        public LoginUseCase(IUserModelRepository userModelRepository, IJwtFactory jwtFactory)
        {
            _userModelRepository = userModelRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> Handle(LoginUserRequest message, IOutputPort<LoginUserResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.UserName) && !string.IsNullOrEmpty(message.Password))
            {
                var user = await _userModelRepository.GetUserByName(message.UserName);
                if (user != null)
                {
                    if (await _userModelRepository.CheckPassword(user, message.Password)) ;
                    {
                        outputPort.Handle(new LoginUserResponse(await _jwtFactory
                            .GenerateEncodedToken(user.Id,user.UserName)));
                        return true;
                    }
                }
            }
            outputPort.Handle(new LoginUserResponse(new []{new Error("login_fail"
                ,"Invalid username or password") }));
            return false;
        }
    }
}