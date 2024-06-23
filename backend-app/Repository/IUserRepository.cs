namespace backend_app.Repository;

public interface IUserRepository
{
    bool AuthorizeUser(string username, string pass);
}