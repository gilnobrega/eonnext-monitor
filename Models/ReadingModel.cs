namespace EonNext.Monitor;

public class ReadingModel
{
    //If its an energy reading or credit reader
    public ReadingType Type { get; init; }
    //Value of reading whether if its in kwh or pence
    public int Value { get; init; }
    //Time when reading happened
    public int Timestamp { get; init; }
}