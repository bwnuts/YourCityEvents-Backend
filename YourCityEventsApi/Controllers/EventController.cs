using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
using YourCityEventsApi.Model;
using YourCityEventsApi.Services;

namespace YourCityEventsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly UserService _userService;

        public EventController(EventService eventService,UserService userService)
        {
            _eventService = eventService;
            _userService = userService;;
        }

        [HttpGet("all")]
        public ActionResult<ResponseModel<List<EventModel>>> GetAll()
        {
            var eventList = _eventService.Get();
            var data = new Dictionary<string, List<EventModel>>();
            data.Add("events", eventList);

            if (eventList != null)
            {
                return new ResponseModel<List<EventModel>>(data);
            }
            
            return new ResponseModel<List<EventModel>>(null,false,new[] {"Unable to get events"});
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<EventModel>>> GetByToken([FromHeader] string Authorization)
        {
            string token = Authorization.Split()[1];
            var city = _userService.Get(token).City;
            var eventList = _eventService.GetByCity(city);
            var data = new Dictionary<string,List<EventModel>>();
            data.Add("events",eventList);
            
            if (eventList != null)
            {
                return new ResponseModel<List<EventModel>>(data);
            }

            return new ResponseModel<List<EventModel>>(null, false, new[] {"Unable to get events"});
        }

        [HttpGet("byCity")]
        public ActionResult<ResponseModel<List<EventModel>>> GetByCity(CityModel cityModel)
        {
            var eventList = _eventService.GetByCity(cityModel);
            
            var data = new Dictionary<string,List<EventModel>>();
            data.Add("events",eventList);
            
            if (eventList != null)
            {
                return new ResponseModel<List<EventModel>>(data);
            }

            return new ResponseModel<List<EventModel>>(null, false, new[] {"Unable to get events"});
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<EventModel>> Get(string id)
        {
            var Event = _eventService.Get(id);
            var data = new Dictionary<string, EventModel>();
            data.Add("event", Event);

            if (Event != null)
            {
                return new ResponseModel<EventModel>(data);
            }

            return new ResponseModel<EventModel>(null, false, new[] {"Event not found"});
        }

        [HttpGet("{id}/visitors")]
        public ActionResult<ResponseModel<List<UserModel>>> GetVisitors(string id)
        {
            var userList = _eventService.GetVisitors(id);
            var data = new Dictionary<string, List<UserModel>>();
            data.Add("users", userList);
            if (userList != null)
            {
                return new ResponseModel<List<UserModel>>(data);
            }

            return new ResponseModel<List<UserModel>>(null, false, new[] {"Unable to get users"});
        }

        [HttpPost]
        public ActionResult<ResponseModel<EventModel>> Create(EventModel eventModel)
        {
            var Event = _eventService.Create(eventModel);
            var data=new Dictionary<string,EventModel>();
            data.Add("event",Event);
            if (Event != null)
            {
                return new ResponseModel<EventModel>(data);
            }
            
            return new ResponseModel<EventModel>(null, false, new[] {"Unable to create event"});

        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<EventModel>> Update(string id, EventModel eventModel)
        {
            var Event = _eventService.Get(id);
            var data=new Dictionary<string,EventModel>();
            data.Add("event",eventModel);
            if (Event != null)
            {
                _eventService.Update(id,eventModel);
                return new ResponseModel<EventModel> (data);
            }
            
            return new ResponseModel<EventModel>(null, false, new[] {"Unable to find event for updating"});
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<string>> Delete(string id)
        {
            var Event = _eventService.Get(id);
            if (Event != null)
            {
                _eventService.Delete(id);
                return new ResponseModel<string>(null);
            }
            
            return new ResponseModel<string>(null, false, new[] {"Unable to find event for deleting"});
        }
    }
}