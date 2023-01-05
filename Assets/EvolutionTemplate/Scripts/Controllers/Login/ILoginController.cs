using Zenject;

public interface ILoginController : IInitializable
{
    void Login();
    void Logout();
    bool IsLoggedIn();
}
