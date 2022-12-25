using GraphQL;
using GraphQL.Client.Http;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient : IAuthenticator
    {
        public string? AuthenticationToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }

        public static GraphQLRequest GenerateAuthenticationTokenRequest(string email, string password) => new GraphQLRequest
        {
            Query = @"
mutation { 
    obtainKrakenToken(input: {email: """ + email + @""", password: """ + password + @"""}) 
    {
        token
        refreshToken
        refreshExpiresIn
    } 
}"
        };

        public static GraphQLRequest GenerateAuthenticationTokenRefreshRequest(string refreshToken) => new GraphQLRequest
        {
            Query = @"
mutation { 
    obtainKrakenToken(input: {refreshToken: """ + refreshToken + @"""}) 
    {
        token
        refreshToken
        refreshExpiresIn
    } 
}"
        };

        public static GraphQLRequest accountInfoRequest = new GraphQLRequest
        {
            Query = @"
query { 
    viewer {
        fullName
        accounts {
            number
        }
    }
}
            "
        };

        public bool IsLoggedIn()
        {
            if (RefreshTokenExpirationDate == null || RefreshToken == null) return false;

            if (DateTimeProvider.GetCurrentDate() > RefreshTokenExpirationDate) return false;

            return true;
        }

        public async Task Login(string? email = null, string? password = null)
        {
            //refreshes authentication token if already logged in
            GraphQLRequest request = IsLoggedIn() ?
                                        GenerateAuthenticationTokenRefreshRequest(RefreshToken) :
                                        GenerateAuthenticationTokenRequest(email, password);

            JObject? obtainKrakenToken = (await GraphQLClient.SendQueryAsync<JObject>(request)).Data["obtainKrakenToken"] as JObject;

            if (obtainKrakenToken == null || obtainKrakenToken["refreshExpiresIn"] == null || obtainKrakenToken["token"] == null) return;

            AuthenticationToken = (string?)obtainKrakenToken["token"];
            RefreshToken = (string?)obtainKrakenToken["refreshToken"];
            RefreshTokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds((int?)obtainKrakenToken["refreshExpiresIn"] ?? 0).UtcDateTime;

            //Updates authorization header in httpclient
            if (GraphQLClient is GraphQLHttpClient client && AuthenticationToken != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthenticationToken);  
            }
        }

        public void Logout()
        {
            AuthenticationToken = null;
            RefreshTokenExpirationDate = null;
            RefreshToken = null;
        }

        public async Task<string?> GetAccountNumber()
        {
            JObject? viewer = (await GraphQLClient.SendQueryAsync<JObject>(accountInfoRequest)).Data["viewer"] as JObject;
            
            if (viewer == null || viewer["accounts"] == null || viewer["accounts"][0] == null) return null;

            return (((string?)viewer["accounts"][0]["number"]));
        }

    }
}
