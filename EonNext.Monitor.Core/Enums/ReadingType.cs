namespace EonNext.Monitor.Core
{
    //Smart Meters will either report readings in energy units (kewh) or credit (if it's pre-paid / pay as you go)
    public enum ReadingType
    {
        Energy,
        Credit
    }
}