namespace EonNext.Monitor.Core
{
    public class Tariff
    {
        //price in pences per kwh
        public int EnergyUnitPrice { get; set; }
        //price base rate per day in pences
        public int DailyBasePrice { get; set; }
        public string MeterId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; } //null if currently active
    }
}