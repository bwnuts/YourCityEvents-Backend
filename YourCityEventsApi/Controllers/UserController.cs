using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Model;
using Microsoft.AspNetCore.Authorization;
using YourCityEventsApi.Services;

namespace YourCityEventsApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("all")]
        public ActionResult<List<UserModel>> Get() =>
            _userService.Get();

        [HttpGet("{token}")]
        public ActionResult<UserModel> Get(string token)=>
            _userService.Get(token);

        [HttpGet("byId/{id}")]
        public ActionResult<UserModel> GetById(string id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpGet("{id}/hostingEvents")]
        public ActionResult<List<EventModel>> GetHostingEvents(string id) =>
            _userService.GetHostingEvents(id);

        [HttpGet("{id}/visitingEvents")]
        public ActionResult<List<EventModel>> GetVisitingEvents(string id) =>
            _userService.GetVisitingEvents(id);

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
