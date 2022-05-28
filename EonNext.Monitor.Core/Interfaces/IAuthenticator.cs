namespace EonNext.Monitor.Core
{
    public interface IAuthenticator
    {
        void Login(string email, string password);
        void Logout();
    }
}