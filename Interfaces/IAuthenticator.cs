namespace EonNext.Monitor;

public interface IAuthenticator
{
    void Login(string email, string password);
    void Logout();
}