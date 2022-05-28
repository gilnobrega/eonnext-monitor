namespace EonNext.Monitor;

public class TariffModel
{
    //price in pences per kwh
    public int EnergyUnitPrice { get; init; }
    //price base rate per day in pences
    public int DailyBasePrice { get; init; }
}