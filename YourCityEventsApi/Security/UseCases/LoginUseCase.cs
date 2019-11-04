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
        private readonly Services.UserService _userService;
        private readonly IJwtFactory _jwtFactory;

        public LoginUseCase(Services.UserService userService, IJwtFactory jwtFactory)
        {
            _userService = userService;
            _jwtFactory = jwtFactory;
        }

        public async Task<bool> Handle(LoginUserRequest message, IOutputPort<LoginUserResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.UserName) && !string.IsNullOrEmpty(message.Password))
            {
                var user =  _userService.Get(message.UserName);
                if (user != null)
                {
                    if ( _userService.CheckPassword(user.Id, message.Password)) ;
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