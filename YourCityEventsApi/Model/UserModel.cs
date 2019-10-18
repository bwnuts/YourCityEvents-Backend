using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourCityEventsApi.Model
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Bio { get; set; }
        
        public string Email { get; set; }

        public CityModel City { get; set; }

        public EventModel[] HostingEvents { get; set; }
        
        public EventModel[] VisitingEvents { get; set; }

        public string ImageUrl { get; set; }

        public UserModel(string id, string userName, string password, string firstName
            , string lastName, string bio, string email, CityModel city, EventModel[] hostingEvents=null
            , EventModel[] visitingEvents=null, string imageUrl=null)
        {
            Id = id;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
            Email = email;
            City = city;
            HostingEvents = hostingEvents;
            VisitingEvents = visitingEvents;
            ImageUrl = imageUrl;
        }
        
        
    }
}