/*using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace YourCityEventsApi.Model
{
    public class ConvertEventModel
    {
        public static EventModel GetEventModel(BackendEventModel backendEventModel)
        {
            var redis = RedisSettings.GetConnectionMultiplexer();
            var _redisUsersDatabase = redis.GetDatabase(0);
            
            var eventModel = EventModel.ConvertToEventModel(backendEventModel);

            var owner = JsonConvert.DeserializeObject<BackendUserModel>(_redisUsersDatabase.StringGet(backendEventModel.Owner));
            var visitors = new List<UserModel>();

            if (backendEventModel.Visitors != null)
            {
                foreach (var id in backendEventModel.Visitors)
                {
                    visitors.Add(ConvertUserModel.GetUserModel(JsonConvert.DeserializeObject<BackendUserModel>(
                        _redisUsersDatabase.StringGet(id))));
                }
            }

            eventModel.Owner = ConvertUserModel.GetUserModel(owner);
            eventModel.Visitors = visitors.ToArray();

            return eventModel;
        }

        public static BackendEventModel GetBackendEventModel(EventModel eventModel)
        {
            var backendEventModel = BackendEventModel.ConvertToBackendEventModel(eventModel);

            var owner = JsonConvert.DeserializeObject<BackendEventModel>(eventModel.Owner.Id);
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
}*/