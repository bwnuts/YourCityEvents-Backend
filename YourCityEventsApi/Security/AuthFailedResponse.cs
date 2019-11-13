using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace YourCityEventsApi.Security
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}