namespace EonNext.Monitor.Core
{
    public class MeterService : IMeterService
    {
        public IMeterRepository MeterRepository { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }

        Consumption IMeterService.GetTotalConsumption()
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetTotalConsumption(DateTime from)
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetTotalConsumption(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
        Consumption IMeterService.GetAverageConsumption(TimeSpan interval)
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetAverageConsumption(TimeSpan interval, DateTime from)
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetAverageConsumption(TimeSpan interval, DateTime from, TimeSpan duration)
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetAverageConsumption(TimeSpan interval, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetRemainingConsumption()
        {
            throw new NotImplementedException();
        }

        Consumption IMeterService.GetRemainingConsumption(DateTime from)
        {
            throw new NotImplementedException();
        }
    }
}