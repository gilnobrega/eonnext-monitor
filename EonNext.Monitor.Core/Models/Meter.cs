namespace EonNext.Monitor.Core
{
    public class Meter
    {
        public string Id { get; set; }
        public string SerialNumber { get; set; }
        public string Mpan { get; set; }
        public virtual ICollection<Tariff> Tariffs { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
        public virtual ICollection<TopUp> TopUps { get; set; }
    }
}