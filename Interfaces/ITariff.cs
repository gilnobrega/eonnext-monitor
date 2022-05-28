namespace eonnext_monitor;

public interface ITariff
{
    //Gets prince in pences per kwh
    int GetUnitPrice();
    //Gets price base rate per day in pences
    int GetBaseDailyPrice();
}