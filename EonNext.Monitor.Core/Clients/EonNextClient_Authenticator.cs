using GraphQL;
using GraphQL.Client.Http;
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

        public static GraphQLRequest accountInfoRequest = new GraphQLRequest
        {
            Query = @"
viewer {
    fullName
    accounts {
        number
    }
}
            "
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

            //Updates authorization header in httpclient
            if (GraphQLClient is GraphQLHttpClient client)
            {
                client.HttpClient.DefaultRequestHeaders.Add("Authorization", AuthenticationToken);
            }
        }

        public void Logout()
        {
            AuthenticationToken = null;
            TokenExpirationDate = null;
        }

        public async Task<(string, string)> GetFullNameAndAccountNumber()
        {
            JObject? viewer = (await GraphQLClient.SendQueryAsync<JObject>(accountInfoRequest)).Data["viewer"] as JObject;

            if (viewer == null) return ("N/A", "N/A");

            string? fullName = ((string?)viewer["fullName"]);
            string? accountNumber = null;

            if (viewer["accounts"] != null && viewer["accounts"][0] != null)
            {
                accountNumber = (((string?)viewer["accounts"][0]["number"]));
            }

            return (fullName ?? "N/A", accountNumber ?? "N/A");
        }

    }
}
