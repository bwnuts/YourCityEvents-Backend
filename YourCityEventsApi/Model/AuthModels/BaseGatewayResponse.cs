using System.Collections.Generic;

namespace YourCityEventsApi.Model.AuthModels
{
    public abstract class BaseGatewayResponse
    {
        public bool Success { get; set; }
        public IEnumerable<Error> Errors { get; set; }

        protected BaseGatewayResponse(bool success = false, IEnumerable<Error> errors = null)
        {
            Success = success;
            Errors = errors;
        }
    }
}