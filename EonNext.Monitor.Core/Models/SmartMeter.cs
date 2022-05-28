namespace EonNext.Monitor.Core
{
    public class SmartMeter
    {
        public string MeterId { get; set; }
        public virtual ICollection<Tarriff> Taiffs { get; set; }
    }
}