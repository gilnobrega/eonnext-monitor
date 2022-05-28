namespace EonNext.Monitor.Core
{
    public interface IReadingRepository
    {
        //Gets active tariff at a given timestamp
        Reading GetReading(int? timestamp = null);
        //Saves reading at given timestamp
        void SaveReading(Reading reading, int? timestamp = null);
    }
}