namespace EonNext.Monitor.Core
{
    public class Config
    {
        //Email used for login
        public string? Email { get; set; }
        //Password used for login
        public string? Password { get; set; }
        //Gets new reading every x minutes
        public int? RefreshInterval { get; set; }
    }
}