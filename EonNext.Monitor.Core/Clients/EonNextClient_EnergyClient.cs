using GraphQL.Client.Abstractions;
using GraphQL;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient : IEnergyClient
    {
        public string FullName { get; private set; }

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

        public static GraphQLRequest activeMeterIdsRequest = new GraphQLRequest
        {
            Query = @"
            "
        };

        public static GraphQLRequest currentTariffRequest = new GraphQLRequest
        {
            Query = @"
            "
        };

        public static GraphQLRequest mostRecentReadingRequest = new GraphQLRequest
        {
            Query = @"
            "
        };

        public static GraphQLRequest mostRecentPaymentRequest = new GraphQLRequest
        {
            Query = @"
            "
        };

        public IMeterRepository MeterRepository { get; set; }
        public IReadingRepository ReadingRepository { get; set; }
        public ITariffRepository TariffRepository { get; set; }
        public ITopUpRepository TopUpRepository { get; set; }
        public IGraphQLClient GraphQLClient { get; set; }

        public List<string> GetActiveMeterIds()
        {
            throw new NotImplementedException();
        }

        public Tariff GetCurrentTariff(string meterId)
        {
            throw new NotImplementedException();
        }

        public TopUp GetMostRecentPayment(string meterId)
        {
            throw new NotImplementedException();
        }

        public Reading GetMostRecentReading(string meterId)
        {
            throw new NotImplementedException();
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

        public void UpdateDatabase()
        {
            throw new NotImplementedException();
        }
    }
}