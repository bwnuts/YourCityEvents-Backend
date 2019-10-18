using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Model;
using YourCityEventsApi.UserService;
using System.Threading.Tasks;
using MongoDB.Driver.Core.Misc;

namespace YourCityEventsApi.Controllers
{
    [Produces("application/json'")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserModelRepository _userRepository;

        public UserController(IUserModelRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private async Task<IEnumerable<UserModel>> GetUsersInternal()
        {
            return await _userRepository.GetAllUsers();
        }
        
        [HttpGet("all")]
        public Task<IEnumerable<UserModel>> Get()
        {
            return GetUsersInternal();
        }

        private async Task<UserModel> GetUserByIdInternal(string id)
        {
            return await _userRepository.GetUserById(id);
        }
        
        [HttpGet("{id}")]
        public Task<UserModel> Get(string id)
        {
            return GetUserByIdInternal(id);
        }

        [HttpPost]
        public void Post(UserModel user)
        {
            _userRepository.CreateUser(user);
        }

        [HttpPut("{id}")]
        public void Put(string id, UserModel user)
        {
            _userRepository.UpdateUser(id, user);
        }

        [HttpDelete("id")]
        public void Delete(string id)
        {
            _userRepository.RemoveUser(id);
        }

        [HttpPost("/post")]
        public void Post()
        {
            UserModel user = new UserModel("1", "biba"
                , "qwerty", "biba", "biba"
                , "bio", "email", new CityModel());
            _userRepository.CreateUser(user);
        }
    }
}