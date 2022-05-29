namespace EonNext.Monitor.Core
{
    public class Reading
    {
        //If its an energy reading or credit reader
        public ReadingType Type { get; set; }
        //Value of reading whether if its in kwh or pence
        public int Value { get; set; }
        //Time when reading happened
        public DateTime Timestamp { get; set; }
        public string MeterId { get; set; }
    }
}