using System.Collections.Generic;

namespace YourCityEventsApi.Model.AuthModels.UseCaseResponses
{
    public sealed class CreateUserResponse:BaseGatewayResponse
    {
        public string Id { get; set; }

        public CreateUserResponse(string id, bool success = false
            , IEnumerable<Error> errors=null) : base(success, errors)
        {
            Id = id;
        }
    }
}