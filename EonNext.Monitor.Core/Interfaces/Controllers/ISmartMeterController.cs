namespace EonNext.Monitor.Core
{
    public interface ISmartMeterController
    {
        //Gets total average consumption
        Consumption GetAverageConsumption();
        //Gets average period from duration to duration
        Consumption GetAverageConsumption(DateTime from, DateTime to);
        Consumption GetAverageConsumption(DateTime from, TimeSpan duration);
    }
}