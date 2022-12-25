using GraphQL;
using Newtonsoft.Json.Linq;

namespace EonNext.Monitor.Core
{
    public partial class EonNextClient : IEnergyClient
    {
        public static GraphQLRequest activeMetersRequest(string accountNumber) => new GraphQLRequest
        {
            Query = @"
query {
    account(accountNumber: """ + accountNumber + @""") {
        properties {
            electricityMeterPoints {
                mpan
                meters(includeInactive: false) {
                    smartDevices {
                        serialNumber
                        deviceId
                    }
                    fuelType
                }
            }
        }
    }
}
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

        public async Task<List<Meter>> GetActiveMeters(string accountNumber)
        {
            JObject? account = (await GraphQLClient.SendQueryAsync<JObject>(activeMetersRequest(accountNumber))).Data["account"] as JObject;
            JArray? properties = account?["properties"] as JArray;
            JArray? electricityMeterPoints = properties?[0]["electricityMeterPoints"] as JArray;

            List<Meter> finalMeters = new List<Meter>();

            if (electricityMeterPoints == null) return finalMeters;

            foreach (JObject points in electricityMeterPoints)
            {
                string mpan = (string)points["mpan"];

                JArray? meters = points["meters"] as JArray;

                foreach (JObject meterData in meters)
                {
                    JArray? smartDevices = meterData["smartDevices"] as JArray;

                    foreach (JObject smartDevice in smartDevices)
                    {
                        Meter meter = new Meter();
                        meter.Mpan = mpan;
                        meter.Id = (string)smartDevice["deviceId"];
                        meter.SerialNumber = (string)smartDevice["serialNumber"];

                        finalMeters.Add(meter);
                    }
                }
            }

            return finalMeters;
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