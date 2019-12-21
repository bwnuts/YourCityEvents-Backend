using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json;
using StackExchange.Redis;
using YourCityEventsApi.Model;

namespace YourCityEventsApi.Services
{
    public class ConvertModelsService
    {
        private IDatabase _redisUsersDatabase;
        private IDatabase _redisEventsDatabase;

        public ConvertModelsService()
        {
            var redis = RedisSettings.GetConnectionMultiplexer();
            _redisUsersDatabase = redis.GetDatabase(0);
            _redisEventsDatabase = redis.GetDatabase(1);
        }

        public UserModel GetUserModel(BackendUserModel backendUserModel)
        {
                var userModel = UserModel.ConvertToUserModel(backendUserModel);

                var hostingEvents = new List<BackendEventModel>();
                var visitingEvents = new List<BackendEventModel>();
                if (backendUserModel.HostingEvents != null)
                {
                    foreach (var id in backendUserModel.HostingEvents)
                    {
                        var Event = JsonConvert.DeserializeObject<BackendEventModel>(
                            _redisEventsDatabase.StringGet(id));
                        Event.Owner = null;
                        Event.Visitors = null;
                        hostingEvents.Add(Event);
                    }
                }

                if (backendUserModel.VisitingEvents != null)
                {
                    foreach (var id in backendUserModel.VisitingEvents)
                    {
                        var Event = JsonConvert.DeserializeObject<BackendEventModel>(
                            _redisEventsDatabase.StringGet(id));
                        Event.Owner = null;
                        Event.Visitors = null;
                        visitingEvents.Add(Event);
                    }
                }

                userModel.HostingEvents = hostingEvents.ToArray();
                userModel.VisitingEvents = visitingEvents.ToArray();

                return userModel;
        }

        public BackendUserModel GetBackendUserModel(UserModel userModel)
        {
            var backendUserModel = BackendUserModel.ConvertToBackendUserModel(userModel);
            
            backendUserModel.Password = JsonConvert.DeserializeObject<BackendUserModel>(
                _redisUsersDatabase.StringGet(backendUserModel.Id)).Password;
            backendUserModel.Token = JsonConvert.DeserializeObject<BackendUserModel>(
                _redisUsersDatabase.StringGet(backendUserModel.Id)).Token;
            
            var hostingEvents = new List<string>();
            var visitingEvents = new List<string>();

            if (userModel.HostingEvents != null)
            {
                foreach (var Event in userModel.HostingEvents)
                {
                    hostingEvents.Add(Event.Id);
                }
            }

            if (userModel.VisitingEvents != null)
            {
                foreach (var Event in userModel.VisitingEvents)
                {
                    visitingEvents.Add(Event.Id);
                }
            }

            backendUserModel.HostingEvents = hostingEvents.ToArray();
            backendUserModel.VisitingEvents = visitingEvents.ToArray();

            return backendUserModel;
        }

        public EventModel GetEventModel(BackendEventModel backendEventModel)
        {
            var eventModel = EventModel.ConvertToEventModel(backendEventModel);

                var owner = JsonConvert.DeserializeObject<BackendUserModel>(
                    _redisUsersDatabase.StringGet(backendEventModel.Owner));
                var visitors = new List<UserModel>();

                if (backendEventModel.Visitors != null)
                {
                    foreach (var id in backendEventModel.Visitors)
                    {
                        visitors.Add(GetUserModel(JsonConvert.DeserializeObject<BackendUserModel>(
                            _redisUsersDatabase.StringGet(id))));
                    }
                }

                eventModel.Owner = GetUserModel(owner);
                eventModel.Visitors = visitors.ToArray();

                return eventModel;
        }

        public BackendEventModel GetBackendEventModel(EventModel eventModel)
        {
            var backendEventModel = BackendEventModel.ConvertToBackendEventModel(eventModel);

            var owner = JsonConvert.DeserializeObject<BackendEventModel>(
                _redisUsersDatabase.StringGet(eventModel.Owner.Id));
            var visitors = new List<string>();

            if (eventModel.Visitors != null)
            {
                foreach (var e in eventModel.Visitors)
                {
                    visitors.Add(e.Id);
                }
            }

            backendEventModel.Owner = owner.Id;
            backendEventModel.Visitors = visitors.ToArray();

            return backendEventModel;
        }
    }
}