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

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<EventModel>>> Get()
        {
           List<EventModel> eventList = _eventService.Get();
           if (eventList != null)
           {
               return new ResponseModel<List<EventModel>>(eventList);
           }

           return new ResponseModel<List<EventModel>>(null, "false", new[] {"Unable to get events"});
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<EventModel>> Get(string id)
        {
            EventModel Event = _eventService.Get(id);
            if (Event != null)
            {
                return new ResponseModel<EventModel>(Event);
            }
            
            return new ResponseModel<EventModel>(null, "false", new[] {"Event not found"});
        }

        [HttpGet("{id}/visitors")]
        public ActionResult<ResponseModel<List<UserModel>>> GetVisitors(string id)
        {
           List<UserModel> userList= _eventService.GetVisitors(id);
           if (userList != null)
           {
               return new ResponseModel<List<UserModel>>(userList, "true");
           }
           
           return new ResponseModel<List<UserModel>>(null, "false", new[] {"Unable to get users"});
        }

        [HttpPost]
        public ActionResult<ResponseModel<EventModel>> Create(EventModel eventModel)
        {
            EventModel Event = _eventService.Create(eventModel);
            if (Event != null)
            {
                return new ResponseModel<EventModel>(Event);
            }
            
            return new ResponseModel<EventModel>(null, "false", new[] {"Unable to create event"});

        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<EventModel>> Update(string id, EventModel eventModel)
        {
            EventModel Event = _eventService.Get(id);
            if (Event != null)
            {
                _eventService.Update(id,eventModel);
                return new ResponseModel<EventModel> (eventModel);
            }
            
            return new ResponseModel<EventModel>(null, "false", new[] {"Unable to find event for updating"});
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<string>> Delete(string id)
        {
            EventModel Event = _eventService.Get(id);
            if (Event != null)
            {
                _eventService.Delete(id);
                return new ResponseModel<string>(null,"Ok");
            }
            
            return new ResponseModel<string>(null, "false", new[] {"Unable to find event for deleting"});
        }
    }
}