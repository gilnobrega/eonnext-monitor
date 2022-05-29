namespace EonNext.Monitor.Core
{
    public interface IMeterRepository
    {
        ICollection<Meter> GetMeters();
    }
}