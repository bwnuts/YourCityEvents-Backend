using System;
using System.Net.Mime;

namespace YourCityEventsApi.Model
{
    public class CreateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageArray { get; set; }
        public int Price { get; set; }
        public CityModel Location { get; set; }
        public string DetailLocation { get; set; }
        public DateTime Date { get; set; }

        public CreateEventRequest(string title, string description, string imageArray
            , int price,CityModel location, string detailLocation, string date)
        {
            Title = title;
            Description = description;
            ImageArray = imageArray;
            Price = price;
            Location = location;
            DetailLocation = detailLocation;
            Date = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm", null);
        }
    }
}
