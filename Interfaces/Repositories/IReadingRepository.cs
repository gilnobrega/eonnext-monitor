namespace EonNext.Monitor;

public interface IReadingRepository
{
    //Gets active tariff at a given timestamp
    ReadingModel GetReading(int? timestamp = null);
    //Saves reading at given timestamp
    void SaveReading(ReadingModel reading, int? timestamp = null);
}