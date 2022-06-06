using GraphQL;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient : IEnergyClient
    {
        public string FullName { get; private set; }

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

        public void UpdateDatabase()
        {
            throw new NotImplementedException();
        }
    }
}