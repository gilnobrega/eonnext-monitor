namespace EonNext.Monitor.Core
{
    public interface ITopUpRepository
    {
        //Gets active tariff at a given timestamp
        List<TopUp> GetTopUp();
        //Saves Tariff at a given timestamp
        void SaveTopUp(TopUp topUp);
    }
}