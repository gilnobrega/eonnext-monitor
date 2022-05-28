namespace EonNext.Monitor.Core
{
    public interface ITariffRepository
    {
        //Gets active tariff at a given timestamp
        Tariff GetTariff(int? timestamp = null);
        //Saves Tariff at a given timestamp
        void SaveTariff(Tariff tariff, int? timestamp = null);
    }
}