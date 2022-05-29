namespace EonNext.Monitor.Core
{
    public interface ITariffRepository
    {
        //Gets active tariff at a given timestamp
        Tariff GetTariff(DateTime? timestamp = null);
        //Saves Tariff at a given timestamp
        void SaveTariff(Tariff tariff, DateTime? timestamp = null);
    }
}