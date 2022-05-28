namespace EonNext.Monitor;

public class ConfigModel
{
    //Email used for login
    public string? Email { get; init; }
    //Password used for login
    public string? Password { get; init; }
    //Gets new reading every x minutes
    public int? RefreshInterval { get; init; }
}