using System.ComponentModel.DataAnnotations;

namespace YourCityEventsApi.Services
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}