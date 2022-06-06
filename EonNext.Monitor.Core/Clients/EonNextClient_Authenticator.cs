using GraphQL.Client.Abstractions;
using GraphQL;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient : IAuthenticator
    {
        public string? AuthenticationToken { get; set; }
        public DateTime? TokenExpirationDate { get; set; }

        public static GraphQLRequest GenerateAuthenticationTokenRequest(string email, string password) => new GraphQLRequest
        {
            Query = @"
  obtainKrakenToken(input: {email: """ + email + @""", password: """ + password + @"""}) 
{
    token
    refreshExpiresIn
}"
        };

        public bool IsLoggedIn()
        {
            if (TokenExpirationDate == null) return false;

            if (DateTimeProvider.GetCurrentDate() > TokenExpirationDate) return false;

            return true;
        }

        public async Task Login(string email, string password)
        {
            if (IsLoggedIn()) return;

            JObject? obtainKrakenToken = (await GraphQLClient.SendQueryAsync<JObject>(GenerateAuthenticationTokenRequest(email, password))).Data["obtainKrakenToken"] as JObject;

            if (obtainKrakenToken == null || obtainKrakenToken["refreshExpiresIn"] == null || obtainKrakenToken["token"] == null) return;

            AuthenticationToken = (string?)obtainKrakenToken["token"];
            TokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds((int?)obtainKrakenToken["refreshExpiresIn"] ?? 0).UtcDateTime;
        }

        public void Logout()
        {
            AuthenticationToken = null;
            TokenExpirationDate = null;
        }

    }
}
