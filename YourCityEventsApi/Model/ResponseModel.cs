using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace YourCityEventsApi.Model
{
    public class ResponseModel<Type> 
    {
        public Type Data { get; set; }
        
        public string Result { get; set; }
        
        public IEnumerable<string> Errors { get; set; }
        
        public ResponseModel (Type data, string result="true", IEnumerable<string> errors=null)
        {
            Data = data;
            Result = result;
            Errors = errors;
        }
    }
}