namespace EonNext.Monitor.Core
{
    //For PAYG meters only
    public class TopUp
    {
        public string MeterId { get; set; }
        //Amount in pence
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}