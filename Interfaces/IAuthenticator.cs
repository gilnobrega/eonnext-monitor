namespace eonnext_monitor;

public interface IAuthenticator
{
    void Login(string email, string password);
}