namespace EonNext.Monitor;

public interface ISmartMeter
{
    //Gets total average consumption
    ConsumptionModel GetAverageConsumption();
    //Gets average period from duration to duration
    ConsumptionModel GetAverageConsumption(DateTime from, DateTime to);
    ConsumptionModel GetAverageConsumption(DateTime from, TimeSpan duration);
}