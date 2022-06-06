namespace EonNext.Monitor.Core
{
    //Connects to EonNext API via GraphQL
    public interface IAuthenticator
    {
        Task Login(string? email = null, string? password = null);
        void Logout();
        bool IsLoggedIn();
        Task<(string, string)> GetFullNameAndAccountNumber();
    }
}