using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourCityEventsApi.Model
{
    public class BackendEventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("location")]
        public CityModel Location { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("owner")]
        public string Owner { get; set; }
        
        [BsonElement("date")]
        public string Date { get; set; }
        
        [BsonElement("image_url")]
        public string ImageUrl { get; set; }
        
        [BsonElement("detail_location")]
        public string DetailLocation { get; set; }
        
        [BsonElement("visitors")]
        public string[] Visitors { get; set; }
        
        [BsonElement("price")]
        public long Price { get; set; }
        
        public BackendEventModel(string id, string title, CityModel location, string detailLocation,string description
            , string owner,string date, long price,string imageUrl = null, string[] visitors=null)
        {
            Id = id;
            Title = title;
            Location = location;
            DetailLocation = detailLocation;
            Description = description;
            Owner = owner;
            Date = date;
            ImageUrl = imageUrl;
            Visitors = visitors;
            Price = price;
        }

        public static BackendEventModel ConvertToBackendEventModel(EventModel eventModel)
        {
            return new BackendEventModel(eventModel.Id,eventModel.Title,eventModel.Location
                ,eventModel.DetailLocation,eventModel.Description,
                null,eventModel.Date,eventModel.Price,
                eventModel.ImageUrl);
        }
    }
}