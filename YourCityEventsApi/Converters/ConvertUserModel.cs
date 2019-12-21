/*using System.Collections.Generic;
using Newtonsoft.Json;

namespace YourCityEventsApi.Model
{
    public class ConvertUserModel
    {
        public static UserModel GetUserModel(BackendUserModel backendUserModel)
        {
            var redis = RedisSettings.GetConnectionMultiplexer();
            var _redisEventsDatabase = redis.GetDatabase(1);
            
            var userModel = UserModel.ConvertToUserModel(backendUserModel);

            var hostingEvents = new List<EventModel>();
            var visitingEvents = new List<EventModel>();

            if (backendUserModel.HostingEvents != null)
            {
                foreach (var id in backendUserModel.HostingEvents)
                {
                    hostingEvents.Add(ConvertEventModel.GetEventModel(JsonConvert.DeserializeObject<BackendEventModel>(
                        _redisEventsDatabase.StringGet(id))));
                }
            }

            if (backendUserModel.VisitingEvents != null)
            {
                foreach (var id in backendUserModel.VisitingEvents)
                {
                    visitingEvents.Add(ConvertEventModel.GetEventModel(JsonConvert.DeserializeObject<BackendEventModel>(
                        _redisEventsDatabase.StringGet(id))));
                }
            }

            userModel.HostingEvents = hostingEvents.ToArray();
            userModel.VisitingEvents = visitingEvents.ToArray();

            return userModel;
        }

        public  static BackendUserModel GetBackendUserModel(UserModel userModel)
        {
            var redis = RedisSettings.GetConnectionMultiplexer();
            var _redisUsersDatabase = redis.GetDatabase(0);
            
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
    }
}*/