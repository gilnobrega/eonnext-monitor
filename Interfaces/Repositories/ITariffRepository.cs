namespace EonNext.Monitor;

public interface ITariffRepository
{
    //Gets active tariff at a given timestamp
    TariffModel GetTariff(int? timestamp = null);
    //Saves Tariff at a given timestamp
    void SaveTariff(TariffModel tariff, int? timestamp = null);
}