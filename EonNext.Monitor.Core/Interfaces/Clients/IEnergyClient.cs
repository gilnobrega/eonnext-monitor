namespace EonNext.Monitor.Core
{
    //Connects to EonNext API via GraphQL
    public interface IEnergyClient
    {
        //Grabs most recent data from API and updates database
        void UpdateDatabase();
        List<Meter> GetActiveMeters(string accountNumber);
        Reading GetMostRecentReading(String meterId);
        Tariff GetCurrentTariff(String meterId);
        TopUp GetMostRecentPayment(String meterId);
    }
}