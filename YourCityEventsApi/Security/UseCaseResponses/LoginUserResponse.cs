using System.Collections.Generic;

namespace YourCityEventsApi.Model.AuthModels.UseCaseResponses
{
    public class LoginUserResponse : UseCaseResponseMessage
    {
        public TokenModel Token { get; set; }
        public IEnumerable<Error> Errors { get; set; }

        public LoginUserResponse(IEnumerable<Error> errors, bool success = false
            , string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public LoginUserResponse(TokenModel token, bool success = false
            , string message = null) : base(success, message)
        {
            Token = token;
        }
    }
}