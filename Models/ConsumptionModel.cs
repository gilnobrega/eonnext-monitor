namespace EonNext.Monitor;

public class ConsumptionModel
{
    //Energy units consumed in kwh
    public int Energy { get; init; }
    //Price units consumed  in pence
    public int Price { get; init; }
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public TimeSpan Duration => To - From;
}