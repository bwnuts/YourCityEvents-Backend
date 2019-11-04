using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace YourCityEventsApi.Model.AuthModels
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}