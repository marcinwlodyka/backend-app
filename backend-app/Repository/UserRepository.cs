namespace backend_app.Repository;

public class UserRepository : IUserRepository
{
    public bool AuthorizeUser(string username, string password)
    {
        return username.ToLower().Equals("wsei") && password.ToLower().Equals("wsei");
    }
}