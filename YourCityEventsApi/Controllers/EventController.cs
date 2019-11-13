using System;
using System.Collections.Generic;
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
        public ActionResult<List<EventModel>> Get() =>
            _eventService.Get();

        [HttpGet("{id}")]
        public ActionResult<EventModel> Get(string id)
        {
            var Event = _eventService.Get(id);
            if (Event == null)
                return NotFound();
            return Event;
        }

        [HttpGet("{id}/visitors")]
        public ActionResult<List<UserModel>> GetVisitors(string id) =>
            _eventService.GetVisitors(id);
        

            [HttpPost]
        public ActionResult<EventModel> Create(EventModel eventModel)
        {
            return _eventService.Create(eventModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, EventModel eventModel)
        {
            var Event = _eventService.Get(id);
            if (Event == null)
                return NotFound();
            _eventService.Update(id,eventModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var Event = _eventService.Get(id);
            if (Event == null)
                return NotFound();
            _eventService.Delete(Event.Id);
            return Ok();
        }
    }
}