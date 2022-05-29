namespace EonNext.Monitor.Core
{
    public interface IReadingRepository
    {
        //Gets active tariff at a given timestamp
        Reading GetReading(DateTime? timestamp = null);
        //Saves reading at given timestamp
        void SaveReading(Reading reading, DateTime? timestamp = null);
    }
}