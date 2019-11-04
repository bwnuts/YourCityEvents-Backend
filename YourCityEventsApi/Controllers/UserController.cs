using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Model;
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

        [HttpGet("{id}")]
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
            return _userService.Create(userModel);
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
            _userService.Delete(user.Id);
            return Ok();
        }
    }
}