using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using YourCityEventsApi.Services;
using YourCityEventsApi.Security;

namespace YourCityEventsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityService _identityService;
        private readonly UserService _userService;

        public IdentityController(IdentityService identityService,UserService userService)
        {
            _identityService = identityService;
            _userService = userService;
        }

        [HttpPost("/register")]
        public IActionResult Register(UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x=>x.Errors.Select(xx=>xx.ErrorMessage))
                });
            }

            var authResponse = _identityService.Register(request.Email, request.Password, request.FirstName
                , request.LastName, request.City);
            
            _userService.AddUserToken(request.Email,authResponse.Token);
            
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                  Errors  = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
        
        [HttpPost("/login")]
        public IActionResult Login(UserLoginRequest request)
        {
            var authResponse = _identityService.Login(request.Email, request.Password);

            _userService.AddUserToken(request.Email,authResponse.Token);
            
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors  = authResponse.Errors
                });
            }
            
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
    
}