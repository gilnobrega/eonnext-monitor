using GraphQL.Client;
using GraphQL.Client.Serializer.SystemTextJson;
using GraphQL.Client.Abstractions;
using GraphQL;

namespace EonNext.Monitor.Core
{
    public class EonNextClient : IEnergyClient
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string FullName { get; private set; }

        private const string _endpoint = "https://api.eonnext-kraken.energy/v1/graphql/";

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

        public (string, string) GetFullNameAndAccountNumber()
        {
            throw new NotImplementedException();
        }

        public void Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void UpdateDatabase()
        {
            throw new NotImplementedException();
        }
    }
}