namespace YourCityEventsApi.Model.AuthModels
{
    public class TokenModel
    {
        public string Id { get; set;}
        public string AuthToken { get; set;}
        public int ExpiresIn { get;set; }

        public TokenModel(string id, string authToken, int expiresIn)
        {
            Id = id;
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }
    }
}