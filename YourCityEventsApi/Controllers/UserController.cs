<<<<<<< HEAD
using System;
=======
>>>>>>> d954089b35a675d5580494283b8206dd0577cf90
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Model;
<<<<<<< HEAD
using System.Threading.Tasks;
using Microsoft.IdentityModel.Xml;
using YourCityEventsApi.Services;

namespace YourCityEventsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Services.UserService _userService;

        public UserController(Services.UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserModel>> Get() =>
            _userService.Get();

        [HttpGet("{id}",Name="GetUser")]
        public ActionResult<UserModel> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost]
        public ActionResult<UserModel> Create(UserModel userModel)
        {
            _userService.Create(userModel);
            return CreatedAtRoute("GetBook", 
                new {id = userModel.Id.ToString()},userModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id,UserModel userModel)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound();
            _userService.Update(id, userModel);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
                return NotFound();
            _userService.Remove(user.Id);
            return Ok();
        }
        /*private async Task<IEnumerable<UserModel>> GetUsersInternal()
        {
            return await _userRepository.GetAllUsers();
        }
        
        [HttpGet("all")]
        public Task<IEnumerable<UserModel>> Get()
        {
            return _userRepository.GetAllUsers();
            //return this.ToJson(_userRepository.GetAllUsers());
            /*List<UserModel> list = new List<UserModel>();
            list.Add(
                new UserModel("1", "biba"
                    , "qwerty", "biba", "biba"
                    , "bio", "email", new CityModel()));
            list.Add(
                new UserModel("2", "biba"
                    , "qwerty", "biba", "biba"
                    , "bio", "email", new CityModel()));
    
            return list;#1#
        }*/

            /*private async Task<UserModel> GetUserByIdInternal(string id)
            {
                return await _userRepository.GetUserById(id);
            }
            
            [HttpGet("{id}")]
            public Task<UserModel> Get(string id)
            {
                Console.WriteLine(GetUserByIdInternal(id));
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
            }*/
=======
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
>>>>>>> d954089b35a675d5580494283b8206dd0577cf90
    }
}