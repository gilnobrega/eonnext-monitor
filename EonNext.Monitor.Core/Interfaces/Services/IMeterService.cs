namespace EonNext.Monitor.Core
{
    public interface IMeterService
    {
        //gets total consumption of all readings
        Consumption GetTotalConsumption();
        //gets total consumption since a specific date
        Consumption GetTotalConsumption(DateTime from);
        //gets total consumption from a given date to a specific date
        Consumption GetTotalConsumption(DateTime from, DateTime to);
        //Gets average consumption of all readings over a given interval
        Consumption GetAverageConsumption(TimeSpan interval);
        //Gets average consumption since a given timestamp over a specific interval
        Consumption GetAverageConsumption(TimeSpan interval, DateTime from);
        //Gets average consumption from timestamp to timestamp        
        Consumption GetAverageConsumption(TimeSpan interval, DateTime from, DateTime to);
        Consumption GetAverageConsumption(TimeSpan interval, DateTime from, TimeSpan duration);
        //How much is left (currently)
        Consumption GetRemainingConsumption();
        //How much was left at a given time
        Consumption GetRemainingConsumption(DateTime from);
    }
}