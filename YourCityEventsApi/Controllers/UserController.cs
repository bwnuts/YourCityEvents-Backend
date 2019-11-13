using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YourCityEventsApi.Model;
using Microsoft.AspNetCore.Authorization;
using YourCityEventsApi.Services;

namespace YourCityEventsApi.Controllers
{
    [Authorize]
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
        public ActionResult<ResponseModel<List<UserModel>>> Get()
        {
            List<UserModel> userList= _userService.Get();
            if (userList != null)
            {
                return new ResponseModel<List<UserModel>>(userList);
            }

            return new ResponseModel<List<UserModel>>(null, "false", new[] {"Unable to get models"});
        }

        [HttpGet]
        public ActionResult<ResponseModel<UserModel>> Get([FromHeader] string Authorization)
        {
            string token = Authorization.Split()[1];
            UserModel user = _userService.Get(token);
            if (user != null)
            {
                return new ResponseModel<UserModel>(user);
            }
            
            return new ResponseModel<UserModel>(null, "false", new[] {"User not found"});

        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<UserModel>> GetById(string id)
        {
            var user = _userService.GetById(id);
            if (user != null)
            {
                return new ResponseModel<UserModel>(user);
            }
            
            return new ResponseModel<UserModel>(null, "false", new[] {"User not found"});

        }

        [HttpGet("{id}/hostingEvents")]
        public ActionResult<ResponseModel<List<EventModel>>> GetHostingEvents(string id)
        {
            List<EventModel> eventList=  _userService.GetHostingEvents(id);
            if (eventList != null)
            {
                return new ResponseModel<List<EventModel>>(eventList);
            }
            
            return new ResponseModel<List<EventModel>>(null, "false", new[] {"Unable to get events"});

        }

        [HttpGet("{id}/visitingEvents")]
        public ActionResult<ResponseModel<List<EventModel>>> GetVisitingEvents(string id)
        {
            List<EventModel> eventList=_userService.GetVisitingEvents(id);
            if (eventList != null)
            {
                return new ResponseModel<List<EventModel>>(eventList);
            }
            
            return new ResponseModel<List<EventModel>>(null, "false", new[] {"Unable to get events"});
        }

        [HttpPost]
        public ActionResult<ResponseModel<UserModel>> Create(UserModel userModel)
        {
            UserModel user= _userService.Create(userModel);
            if (user != null)
            {
                return new ResponseModel<UserModel>(user);
            }
            
            return new ResponseModel<UserModel>(null, "false", new[] {"Unable to create user"});
        }
        
        [HttpPut("{id}")]
        public ActionResult<ResponseModel<UserModel>> Update(string id,UserModel userModel)
        {
            var user = _userService.Get(id);
            if (user != null)
            {
                _userService.Update(id,userModel);
                return new ResponseModel<UserModel>(userModel);
            }
            
            return new ResponseModel<UserModel>(null, "false", new[] {"Unable to find user for updating"});
        }
        
        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<string>> Delete(string id)
        {
            var user = _userService.Get(id);
            if (user != null)
            {
                _userService.Delete(id);
                return new ResponseModel<string>(null, "Ok");
            }

            return new ResponseModel<string>(null, "false", new[] {"Unable to find user for deleting"});
        }
    }
}