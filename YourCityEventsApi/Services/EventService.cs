using System;
using MongoDB.Driver;
using YourCityEventsApi.Model;
using System.Collections.Generic;

namespace YourCityEventsApi.Services
{
    public class EventService
    {
        private IMongoCollection<EventModel> _events;

        public EventService(IDatabaseSettings settings)
        {
            var client=new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<EventModel>("Events");
        }

        public List<EventModel> Get()
        {
            return _events.Find(Event => true).ToList();
        }
            


        public EventModel Get(string id) =>
            _events.Find(Event => Event.Id == id).FirstOrDefault();

        public EventModel GetByTitle(string title) =>
            _events.Find(Event => Event.Title == title).FirstOrDefault();

        public EventModel Create(EventModel eventModel)
        {
            _events.InsertOne(eventModel);
            return GetByTitle(eventModel.Title);
        }

        public void Update(string id, EventModel eventModel) =>
            _events.ReplaceOne(Event => Event.Id == id, eventModel);

        public void Delete(string id) =>
            _events.DeleteOne(Event => Event.Id == id);
    }
}