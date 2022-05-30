namespace EonNext.Monitor.Core
{
    public interface IReadingRepository
    {
        //Gets all readings
        List<Reading> GetReadings();
        //Saves reading at given timestamp
        void SaveReading(Reading reading);
    }
}