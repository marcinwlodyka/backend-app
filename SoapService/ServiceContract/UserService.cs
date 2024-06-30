using SoapService.DataContract;

namespace SoapService.ServiceContract;

public class UserService : IUserService
{
    public string RegisterUser(User user)
    {
        return Validate(user)
            ? $"User {user.EmailAddress} registered!"
            : "Cannot register user.";
    }

    private bool Validate(User user)
    {
        if (user == null)
            return false;

        return !string.IsNullOrEmpty(user.EmailAddress);
    }
}