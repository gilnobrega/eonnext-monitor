namespace EonNext.Monitor.Core
{
    //Connects to EonNext API via GraphQL
    public interface IEnergyClient
    {
        void Login(string email, string password);
        void Logout();
        //Grabs most recent data from API and updates database
        void UpdateDatabase();
        List<String> GetActiveMeterIds();
        Reading GetMostRecentReading(String meterId);
        Tariff GetCurrentTariff(String meterId);
        TopUp GetMostRecentPayment(String meterId);
    }
}