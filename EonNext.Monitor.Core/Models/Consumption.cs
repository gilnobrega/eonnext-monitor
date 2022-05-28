namespace EonNext.Monitor.Core
{
    public class Consumption
    {
        //Energy units consumed in kwh
        public int Energy { get; set; }
        //Price units consumed  in pence
        public int Price { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TimeSpan Duration => To - From;
    }
}