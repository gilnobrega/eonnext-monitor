namespace EonNext.Monitor.Core
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
